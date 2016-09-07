using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utils
{
  public class BitmapUtils
  {
    public BitmapSource ToDefaultDpi(BitmapSource source)
    {
      var notEquals = source != null && (DefaultDpi.Equals(source.DpiX)
                                       || DefaultDpi.Equals(source.DpiY));
      return notEquals ? ChangeDpi(source) : source;
    }
    public BitmapImage LoadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
        return null;

      var image = new BitmapImage();
      image.BeginInit();
      image.UriSource = new Uri(fileName);
      image.CacheOption = BitmapCacheOption.OnLoad;
      image.EndInit();

      return image;
    }

    public Bitmap CropBitmap(Bitmap srcBitmap, Rectangle section)
    {
      var bmp = new Bitmap(section.Width, section.Height);
      using (var g = Graphics.FromImage(bmp))
      {
        g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
      }

      return bmp;
    }
    private static BitmapSource ChangeDpi(BitmapSource sourceImage)
    {
      if (sourceImage == null) return null;

      var width = sourceImage.PixelWidth;
      var height = sourceImage.PixelHeight;

      var stride = width * sourceImage.Format.BitsPerPixel / 8; // 4 bytes per pixel
      var pixelData = new byte[stride * height];
      sourceImage.CopyPixels(pixelData, stride, 0);

      return BitmapSource.Create(width, height, DefaultDpi, DefaultDpi, PixelFormats.Bgra32, null, pixelData, stride);
    }

    private const double DefaultDpi = 96;
  }
}
