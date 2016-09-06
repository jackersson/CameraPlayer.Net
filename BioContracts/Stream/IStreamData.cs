using System.Drawing;

namespace BioContracts.Stream
{
  public enum StreamType
  {
      StreamTypeColor = 1
    , StreamTypeDepth = 2
  }

  public interface IStreamData
  {
    bool TryGetBitmap(StreamType type, out Bitmap bitmap);
  }
}
