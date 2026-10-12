using System.Net.Http.Headers;

namespace ZooStav.Wolf.Web.Infrastructure;


public class WebcamService
{
    private readonly WebcamOptions _o;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpClientFactory _httpFactory;
    private readonly ILogger<WebcamService> _log;
    private readonly SemaphoreSlim _snapshotLock = new(1, 1);

    private byte[]? _snapshotCache;
    private DateTime _snapshotCacheUtc = DateTime.MinValue;
    private DateTime? _lastFrameUtc;
    private DateTime? _lastCameraFrameUtc;
    private string? _lastError;
    private string? _transcodeError;

    public WebcamService(WebcamOptions options, IWebHostEnvironment env,
        IHttpClientFactory httpFactory, ILogger<WebcamService> log)
    {
        _o = options;
        _env = env;
        _httpFactory = httpFactory;
        _log = log;
    }

    public WebcamOptions Options => _o;
    public string HlsDirectoryPath
    {
        get
        {
            var dir = Path.IsPathRooted(_o.HlsDirectory)
                ? _o.HlsDirectory
                : Path.Combine(_env.ContentRootPath, _o.HlsDirectory);
            Directory.CreateDirectory(dir);
            return dir;
        }
    }

    public string HlsPlaylistUrl => $"/hls/{_o.HlsPlaylistName}";
    public bool LocalPlaylistExists => File.Exists(Path.Combine(HlsDirectoryPath, _o.HlsPlaylistName));

    public bool LocalPlaylistFresh
    {
        get
        {
            var path = Path.Combine(HlsDirectoryPath, _o.HlsPlaylistName);
            if (!File.Exists(path)) return false;
            return DateTime.UtcNow - File.GetLastWriteTimeUtc(path) < TimeSpan.FromSeconds(60);
        }
    }

    public void ReportTranscodeError(string? error) => _transcodeError = error;
    public void ClearTranscodeError() => _transcodeError = null;

    public WebcamStatus GetStatus()
    {
        var status = new WebcamStatus
        {
            Location = _o.Location,
            PosterPath = _o.PosterPath,
            PollSeconds = Math.Clamp(_o.PollSeconds, 1, 60),
            TranscodeError = _transcodeError,
            LastError = _lastError,
            LastFrameUtc = _lastFrameUtc
        };

        var mode = ResolveMode();
        status.Mode = mode;
        status.Transcoding = !string.IsNullOrWhiteSpace(_o.CameraInput);

        switch (mode)
        {
            case "hls":
                status.IsLive = true;
                status.StreamUrl = !string.IsNullOrWhiteSpace(_o.StreamUrl) ? _o.StreamUrl : HlsPlaylistUrl;
                status.SourceDescription = !string.IsNullOrWhiteSpace(_o.StreamUrl)
                    ? $"поток камеры (HLS): {_o.StreamUrl}"
                    : $"поток с камеры {_o.CameraInput} (транскодирование в HLS средствами приложения)";
                break;
            case "mjpeg":
                status.IsLive = true;
                status.MjpegProxyUrl = "/api/webcam/mjpeg";
                status.SourceDescription = $"MJPEG-поток камеры: {_o.MjpegUrl}";
                break;
            case "snapshot":
                status.IsLive = true;
                status.SnapshotProxyUrl = "/api/webcam/snapshot";
                status.SourceDescription = $"кадры камеры раз в {Math.Clamp(_o.PollSeconds, 1, 60)} с: {_o.SnapshotUrl}";
                break;
            default:
                status.IsLive = false;
                status.SnapshotProxyUrl = "/api/webcam/snapshot";
                status.SourceDescription = "демонстрационные кадры (камера не подключена)";
                break;
        }

        return status;
    }

    public string ResolveMode()
    {
        var mode = (_o.Mode ?? "auto").Trim().ToLowerInvariant();
        if (mode is "hls" or "mjpeg" or "snapshot" or "demo")
        {
 
            return mode switch
            {
                "hls" when !string.IsNullOrWhiteSpace(_o.StreamUrl) || !string.IsNullOrWhiteSpace(_o.CameraInput) || LocalPlaylistFresh => "hls",
                "mjpeg" when !string.IsNullOrWhiteSpace(_o.MjpegUrl) => "mjpeg",
                "snapshot" when !string.IsNullOrWhiteSpace(_o.SnapshotUrl) => "snapshot",
                "hls" or "mjpeg" or "snapshot" => "demo",
                _ => "demo"
            };
        }

        if (!string.IsNullOrWhiteSpace(_o.StreamUrl)) return "hls";
        if (!string.IsNullOrWhiteSpace(_o.CameraInput) || LocalPlaylistFresh) return "hls";
        if (!string.IsNullOrWhiteSpace(_o.MjpegUrl)) return "mjpeg";
        if (!string.IsNullOrWhiteSpace(_o.SnapshotUrl)) return "snapshot";
        return "demo";
    }

    public async Task<(byte[] Data, string ContentType, bool FromCamera)> GetSnapshotAsync(CancellationToken ct)
    {
        var mode = ResolveMode();
        var cacheSeconds = Math.Clamp(_o.PollSeconds, 1, 60);

        if (mode is "snapshot" or "mjpeg" && !string.IsNullOrWhiteSpace(_o.SnapshotUrl))
        {
            await _snapshotLock.WaitAsync(ct);
            try
            {
                if (_snapshotCache != null && DateTime.UtcNow - _snapshotCacheUtc < TimeSpan.FromSeconds(cacheSeconds))
                    return (_snapshotCache, "image/jpeg", true);

                try
                {
                    var client = CreateCameraClient();
                    using var resp = await client.GetAsync(_o.SnapshotUrl, HttpCompletionOption.ResponseContentRead, ct);
                    resp.EnsureSuccessStatusCode();
                    var bytes = await resp.Content.ReadAsByteArrayAsync(ct);
                    if (bytes.Length > 0)
                    {
                        _snapshotCache = bytes;
                        _snapshotCacheUtc = DateTime.UtcNow;
                        _lastCameraFrameUtc = DateTime.UtcNow;
                        _lastFrameUtc = DateTime.UtcNow;
                        _lastError = null;
                        var contentType = resp.Content.Headers.ContentType?.MediaType ?? "image/jpeg";
                        return (bytes, contentType, true);
                    }
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _lastError = $"{DateTime.Now:HH:mm:ss} — камера недоступна: {ex.Message}";
                    _log.LogWarning(ex, "Не удалось получить кадр с камеры {Url}", _o.SnapshotUrl);
                }
            }
            finally
            {
                _snapshotLock.Release();
            }
        }


        var placeholder = await ReadPlaceholderAsync(ct);
        _lastFrameUtc = DateTime.UtcNow;
        return (placeholder, "image/jpeg", false);
    }

    public HttpClient CreateCameraClient()
    {
        var client = _httpFactory.CreateClient("camera");
        client.Timeout = TimeSpan.FromSeconds(10);
        if (!string.IsNullOrEmpty(_o.HttpUser))
        {
            var raw = $"{_o.HttpUser}:{_o.HttpPassword}";
            var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(raw));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        }
        return client;
    }

    private async Task<byte[]> ReadPlaceholderAsync(CancellationToken ct)
    {
        var path = Path.Combine(_env.WebRootPath, _o.PlaceholderPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(path))
        {
            try { return await File.ReadAllBytesAsync(path, ct); }
            catch (Exception ex) { _log.LogWarning(ex, "Не удалось прочитать демо-кадр {Path}", path); }
        }

        return Array.Empty<byte>();
    }
}
