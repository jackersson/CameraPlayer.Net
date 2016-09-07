using System.Collections.Generic;
using System.Linq;
using Accord.Video.DirectShow;
using BioContracts.Stream;

namespace BioCaptureDevices.Common
{
  public class Utils
  {
    public List<IMediaFormat> Formats(VideoCaptureDevice videoSource)
    {
      var result = videoSource?.VideoCapabilities;
      if (result == null || result.Length <= 0)
        return null;

      return videoSource.VideoCapabilities.Select(vc => new BasicMediaFormat()
      {
        FrameWidth = vc.FrameSize.Width
       ,
        FrameHeight = vc.FrameSize.Height
      }).Cast<IMediaFormat>().ToList();
    }

    public VideoCapabilities BestResolution(VideoCaptureDevice videoSource)
    {
      var all = videoSource?.VideoCapabilities;
      if (all == null)
        return null;

      var best = videoSource.VideoCapabilities.FirstOrDefault();
      if (best == null)
        return null;
     

      foreach (var vc in all)
      {
        if (  best.FrameSize.Height * best.FrameSize.Width 
            < vc.FrameSize.Width * vc.FrameSize.Height)
          best = vc;
      }

      return best;
    }

    public bool IsEqual(VideoCapabilities videoCapabilities, IMediaFormat format)
    {
      if (videoCapabilities == null || format == null)
        return false;

      var frameSize = videoCapabilities.FrameSize;
      return format.FrameHeight == frameSize.Height
             && format.FrameWidth == frameSize.Width;
    }
  }
}
