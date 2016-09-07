using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace Utils
{
  public static class BitmapConversion
  {
    public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
    {
      if (bitmap == null) return null;
      using (var memoryStream = new MemoryStream())
      {
        bitmap.Save(memoryStream, ImageFormat.Jpeg);
        memoryStream.Position = 0;
        var bitmapImage = new BitmapImage();

        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memoryStream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        return bitmapImage;
      }
    }

    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    public static BitmapSource BitmapToBitmapSource(Bitmap source)
    {
      if (source == null)
        return null;

      BitmapSource bmp = null;
      try
      {
        var hBitMap = source.GetHbitmap();
        bmp = Imaging.CreateBitmapSourceFromHBitmap(hBitMap, IntPtr.Zero, Int32Rect.Empty,
          BitmapSizeOptions.FromEmptyOptions());
        DeleteObject(hBitMap);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex); //TODO handle
      }
      return bmp;
    }

    public static Bitmap BitmapSourceToBitmap(BitmapSource source)
    {
      if (source == null)
        return null;

      var newSource = new FormatConvertedBitmap();
      newSource.BeginInit();
      newSource.Source = source;
      newSource.DestinationFormat = source.Format;
      newSource.EndInit();

      var bitmap = new Bitmap(newSource.PixelWidth
        , newSource.PixelHeight
        , PixelFormat.Format32bppArgb);

      var data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size)
        , ImageLockMode.WriteOnly
        , PixelFormat.Format32bppArgb);

      try
      {
        newSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
      }
      catch (Exception e)
      {
        Console.Write(e); //TODO handle
      }

      bitmap.UnlockBits(data);

      return bitmap;
    }
  }
}
