using System.Collections.Generic;
using System.Drawing;
using BioContracts.Stream;

namespace BioCaptureDevices.Common
{
  public class StreamData : IStreamData
  {
    public StreamData()
    {
      _data = new Dictionary<StreamType, Bitmap>();
    }
    public void AddFrame(Bitmap bitmap, StreamType type)
    {
      if (bitmap == null) return;
      _data.Add(type, bitmap);
    }

    public bool TryGetBitmap(StreamType type, out Bitmap bitmap)
    {
      return _data.TryGetValue(type, out bitmap);
    }

    private readonly Dictionary<StreamType, Bitmap> _data;
  }
}
