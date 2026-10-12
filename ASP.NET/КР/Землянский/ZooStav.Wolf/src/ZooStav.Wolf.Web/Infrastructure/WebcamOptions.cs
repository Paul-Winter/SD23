namespace ZooStav.Wolf.Web.Infrastructure;

public class WebcamOptions
{
   
    public string Mode { get; set; } = "auto";

    public string StreamUrl { get; set; } = string.Empty;
    public string CameraInput { get; set; } = string.Empty;
    public bool CameraInputRealtime { get; set; } = true;
    public string CameraInputArguments { get; set; } = string.Empty;
    public string MjpegUrl { get; set; } = string.Empty;
    public string SnapshotUrl { get; set; } = string.Empty;
    public string HttpUser { get; set; } = string.Empty;
    public string HttpPassword { get; set; } = string.Empty;
    public string HlsDirectory { get; set; } = "hls";
    public string HlsPlaylistName { get; set; } = "wolf.m3u8";
    public string FfmpegPath { get; set; } = "ffmpeg";
    public bool TranscodeFallback { get; set; } = true;

    public string Location { get; set; } = "Вольер «Северная тундра», камера №2";
    public int PollSeconds { get; set; } = 3;

    public string PosterPath { get; set; } = "/images/wolf-webcam.jpg";
    public string PlaceholderPath { get; set; } = "/media/webcam-current.jpg";
}

public class WebcamStatus
{
    public string Mode { get; set; } = "demo";
    public bool IsLive { get; set; }
    public string Location { get; set; } = string.Empty;
    public string PosterPath { get; set; } = string.Empty;
    public string? StreamUrl { get; set; }
    public string? MjpegProxyUrl { get; set; }
    public string? SnapshotProxyUrl { get; set; }
    public bool Transcoding { get; set; }
    public string? TranscodeError { get; set; }
    public DateTime? LastFrameUtc { get; set; }
    public string? LastError { get; set; }
    public int PollSeconds { get; set; } = 3;
    public string SourceDescription { get; set; } = "демонстрационные кадры (камера не подключена)";
}
