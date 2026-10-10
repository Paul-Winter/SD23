using Microsoft.Extensions.Options;
using ZooStav.Wolf.Web.Infrastructure;

namespace ZooStav.Wolf.Web.Api;

/// <summary>
/// Точки доступа к камере вольера: статус, одиночный кадр, проксирование MJPEG,
/// раздача HLS-сегментов (их создаёт ffmpeg из RTSP-потока камеры).
/// </summary>
public static class WebcamEndpoints
{
    public static void Map(WebApplication app)
    {
        var g = app.MapGroup("/api/webcam").WithTags("Веб-камера вольера");

        // ---------------------------------------------------------------- статус
        g.MapGet("/status", (WebcamService cam, IOptions<WebcamOptions> options) =>
        {
            var status = cam.GetStatus();
            var o = options.Value;
            return Results.Ok(new
            {
                status.Mode,
                status.IsLive,
                status.Location,
                status.SourceDescription,
                status.StreamUrl,
                status.MjpegProxyUrl,
                status.SnapshotProxyUrl,
                status.Transcoding,
                status.TranscodeError,
                status.LastFrameUtc,
                status.PollSeconds,
                status.PosterPath,
                Settings = new
                {
                    o.Mode,
                    hasStreamUrl = !string.IsNullOrWhiteSpace(o.StreamUrl),
                    hasCameraInput = !string.IsNullOrWhiteSpace(o.CameraInput),
                    hasMjpegUrl = !string.IsNullOrWhiteSpace(o.MjpegUrl),
                    hasSnapshotUrl = !string.IsNullOrWhiteSpace(o.SnapshotUrl),
                    hlsPlaylistUrl = cam.HlsPlaylistUrl,
                    localPlaylistReady = cam.LocalPlaylistFresh
                },
                HowToConnect = new[]
                {
                    "HLS: Webcam__StreamUrl = https://камера/wolf/index.m3u8 (камера или медиасервер отдаёт HLS сам)",
                    "RTSP → HLS: Webcam__CameraInput = rtsp://user:pass@192.168.1.64:554/Streaming/Channels/101 (приложение поднимет ffmpeg)",
                    "MJPEG: Webcam__MjpegUrl = http://user:pass@192.168.1.64/video (поток проксируется через /api/webcam/mjpeg)",
                    "JPEG-кадры: Webcam__SnapshotUrl = http://user:pass@192.168.1.64/snapshot.jpg (обновление раз в Webcam__PollSeconds секунд)"
                }
            });
        })
        .AllowAnonymous()
        .WithSummary("Состояние камеры: режим, живой ли поток, куда подключаться");

        // ---------------------------------------------------------------- кадр
        g.MapGet("/snapshot", async (HttpContext http, WebcamService cam, CancellationToken ct) =>
        {
            var (data, contentType, fromCamera) = await cam.GetSnapshotAsync(ct);
            if (data.Length == 0) return Results.NotFound(new ApiError("Кадр недоступен"));

            // Кадр нельзя кэшировать — иначе браузер будет показывать «застывшую» картинку.
            http.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
            return Results.Bytes(data, contentType,
                fileDownloadName: $"webcam-{DateTime.Now:yyyyMMdd-HHmmss}.jpg");
        })
        .AllowAnonymous()
        .WithSummary("Текущий кадр вольера (JPEG). С камеры — если задан Webcam:SnapshotUrl, иначе демо-кадр");

        // ---------------------------------------------------------------- MJPEG
        g.MapGet("/mjpeg", async (WebcamService cam, CancellationToken ct) =>
        {
            var url = cam.Options.MjpegUrl;
            if (string.IsNullOrWhiteSpace(url))
                return Results.NotFound(new ApiError("MJPEG-поток не настроен (Webcam:MjpegUrl пуст)"));

            var client = cam.CreateCameraClient();
            var upstream = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);
            if (!upstream.IsSuccessStatusCode)
                return Results.Json(new ApiError($"Камера вернула {(int)upstream.StatusCode} для {url}"),
                    statusCode: StatusCodes.Status502BadGateway);

            // Проксируем multipart/x-mixed-replace «как есть», не буферизуя поток.
            var stream = await upstream.Content.ReadAsStreamAsync(ct);
            return Results.Stream(stream, upstream.Content.Headers.ContentType?.ToString() ?? "multipart/x-mixed-replace; boundary=--myboundary");
        })
        .AllowAnonymous()
        .WithSummary("Прокси MJPEG-потока камеры (multipart/x-mixed-replace) — для <img src=\"/api/webcam/mjpeg\">");

        // ---------------------------------------------------------------- HLS
        // Раздача сегментов, которые пишет ffmpeg (или другой транскодер) в каталог Webcam:HlsDirectory.
        app.MapGet("/hls/{**file}", (string? file, WebcamService cam) =>
        {
            if (string.IsNullOrWhiteSpace(file)) return Results.NotFound();

            var root = Path.GetFullPath(cam.HlsDirectoryPath);
            var full = Path.GetFullPath(Path.Combine(root, file));

            // Защита от выхода за пределы каталога (../).
            if (!full.StartsWith(root, StringComparison.OrdinalIgnoreCase) || !File.Exists(full))
                return Results.NotFound(new ApiError("Файл потока не найден — транскодер ещё не создал сегменты"));

            var contentType = Path.GetExtension(full).ToLowerInvariant() switch
            {
                ".m3u8" => "application/vnd.apple.mpegurl",
                ".ts" => "video/mp2t",
                ".m4s" or ".mp4" => "video/mp4",
                ".key" => "application/octet-stream",
                _ => "application/octet-stream"
            };

            // Плейлисты кэшировать нельзя — иначе плеер «застынет» на старом окне сегментов.
            var stream = new FileStream(full, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);   // ffmpeg дописывает файл во время чтения
            return Results.File(stream, contentType,
                enableRangeProcessing: true,
                lastModified: System.IO.File.GetLastWriteTimeUtc(full));
        })
        .AllowAnonymous()
        .WithTags("Веб-камера вольера")
        .WithSummary("HLS-плейлист и сегменты потока (/hls/wolf.m3u8)");
    }
}
