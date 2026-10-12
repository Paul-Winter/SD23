using System.Diagnostics;

namespace ZooStav.Wolf.Web.Infrastructure;

public class WebcamStreamService : BackgroundService
{
    private readonly WebcamOptions _o;
    private readonly WebcamService _webcam;
    private readonly ILogger<WebcamStreamService> _log;

    public WebcamStreamService(WebcamOptions options, WebcamService webcam, ILogger<WebcamStreamService> log)
    {
        _o = options;
        _webcam = webcam;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(_o.CameraInput))
        {
            _log.LogInformation("Webcam:CameraInput не задан — транскодирование потока не требуется " +
                                "(страница работает в режиме HLS-адреса, MJPEG, JPEG-кадров или демо).");
            return;
        }

        _webcam.ClearTranscodeError();
        var attempt = 0;

        while (!stoppingToken.IsCancellationRequested)
        {
            var useTranscode = attempt % 2 == 1;     
            try
            {
                var exitCode = await RunFfmpegAsync(useTranscode, stoppingToken);
                if (stoppingToken.IsCancellationRequested) break;

                _log.LogWarning("ffmpeg завершился с кодом {Code} ({Mode}). Перезапуск через 5 секунд.",
                    exitCode, useTranscode ? "перекодирование" : "копирование");
                _webcam.ReportTranscodeError(
                    $"ffmpeg завершился с кодом {exitCode} — проверьте доступность источника {_o.CameraInput} " +
                    $"(попытка {(useTranscode ? "с перекодированием" : "копированием потока")}).");
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Ошибка при запуске ffmpeg. Проверьте Webcam:FfmpegPath.");
                _webcam.ReportTranscodeError(
                    $"Не удалось запустить ffmpeg ({_o.FfmpegPath}): {ex.Message}. " +
                    "Установите ffmpeg или укажите полный путь в Webcam:FfmpegPath.");
                break;  
            }

            attempt++;
            try { await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); }
            catch (OperationCanceledException) { break; }
        }

        _log.LogInformation("Служба транскодирования камеры остановлена.");
    }

    private async Task<int> RunFfmpegAsync(bool transcode, CancellationToken ct)
    {
        var dir = _webcam.HlsDirectoryPath;
        var playlist = Path.Combine(dir, _o.HlsPlaylistName);
        var segmentPattern = Path.Combine(dir, "seg_%05d.ts");

       
        var inputArgs = !string.IsNullOrWhiteSpace(_o.CameraInputArguments)
            ? _o.CameraInputArguments
            : (_o.CameraInput.StartsWith("rtsp", StringComparison.OrdinalIgnoreCase)
                ? "-rtsp_transport tcp -stimeout 5000000"
                : string.Empty);

        var realtime = _o.CameraInputRealtime && !_o.CameraInput.StartsWith("rtsp", StringComparison.OrdinalIgnoreCase)
            ? "-re "
            : string.Empty;

        var videoCodec = transcode
            ? "-c:v libx264 -preset veryfast -tune zerolatency -g 50 -sc_threshold 0 -pix_fmt yuv420p -b:v 2500k"
            : "-c:v copy";

        var arguments =
            $"-hide_banner -loglevel warning -nostdin {realtime}" +
            (string.IsNullOrWhiteSpace(inputArgs) ? "" : inputArgs + " ") +
            $"-i \"{_o.CameraInput}\" -an {videoCodec} -f hls -hls_time 2 -hls_list_size 6 " +
            $"-hls_flags delete_segments+independent_segments -hls_segment_filename \"{segmentPattern}\" \"{playlist}\"";

        _log.LogInformation("Запуск ffmpeg ({Mode}): {Args}", transcode ? "перекодирование" : "копирование", arguments);

        var psi = new ProcessStartInfo(_o.FfmpegPath, arguments)
        {
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = psi, EnableRaisingEvents = true };
        process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                _log.LogDebug("ffmpeg: {Line}", e.Data);
                _webcam.ReportTranscodeError($"ffmpeg: {e.Data}");
            }
        };

        if (!process.Start())
            throw new InvalidOperationException("Процесс ffmpeg не запустился");

        process.BeginErrorReadLine();

       
        using var cancellationRegistration = ct.Register(() =>
        {
            try
            {
                if (!process.HasExited) process.Kill(entireProcessTree: true);
            }
            catch (Exception ex)
            {
                _log.LogDebug(ex, "Не удалось остановить ffmpeg");
            }
        });

        _webcam.ClearTranscodeError();

       
        var startedAt = DateTime.UtcNow;
        while (!ct.IsCancellationRequested && !process.HasExited)
        {
            if (DateTime.UtcNow - startedAt > TimeSpan.FromSeconds(8) && File.Exists(playlist))
            {
                _webcam.ClearTranscodeError();
                _log.LogInformation("Поток HLS создан: {Playlist}", playlist);
                break;
            }
            await Task.Delay(500, ct);
        }

        await process.WaitForExitAsync(ct);
        return process.ExitCode;
    }
}
