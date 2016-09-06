using AForge.Video;

namespace BioCaptureDevices.Common
{
  public class CaptureDeviceErrorInfo
  {
    public static string GetErrorMessage(ReasonToFinishPlaying error)
    {
      string szMessage;
      switch (error)
      {
        case ReasonToFinishPlaying.EndOfStreamReached:
          szMessage = "End of stream reached";
          break;

        case ReasonToFinishPlaying.StoppedByUser:
          szMessage = "Stopped by user";
          break;

        case ReasonToFinishPlaying.DeviceLost:
          szMessage = "Device lost";
          break;

        case ReasonToFinishPlaying.VideoSourceError:
          szMessage = "Video source error";
          break;

        default:
          szMessage = $"Error code: {error}";
          break;
      }
      return szMessage;
    }
  }
}
