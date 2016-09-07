using System.Drawing;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace ImageViewer.Contracts
{
  public interface IImageSource
  {
    Bitmap Bitmap { get; }
  }

  public interface IImageItemScreen : IScreen
  {
    BitmapSource ImageSource { get; set; }
    void Zoom(double width, double height);
    bool HasImage { get; }
    void ShowTopLayer(bool flag);
  }
}
