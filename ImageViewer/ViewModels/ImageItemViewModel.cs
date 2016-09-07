using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BioContracts.Logging;
using Caliburn.Micro;
using Castle.Windsor;
using ImageViewer.Contracts;
using Utils;

namespace ImageViewer.ViewModels
{
  public class ImageItemViewModel : Screen, IImageItemScreen, IImageSource
  {
    public ImageItemViewModel(IWindsorContainer container)
    {
      BitmapUtils = new BitmapUtils();
      TryResolveDependencies(container);
    }

    private void TryResolveDependencies(IWindsorContainer container)
    {
      try
      {
        _logger     = container.Resolve<ILogger>();
        _dispatcher = container.Resolve<Dispatcher>();
        _dispatcher.ShutdownStarted += OnDispatcherShutdownStarted;
      }
      catch (Exception exception) {
        _logger?.Error(exception);
      }
    }

    private void OnDispatcherShutdownStarted(object sender, EventArgs e)
    {
      _isShutDownStarted = true;
    }

    public void Zoom(double newControlWidth, double newControlHeight)
    {
      if (ControlHeight.Equals(newControlHeight) && ControlWidth.Equals(newControlWidth))
        return;

      ControlHeight = newControlHeight * ZoomRatio;
      ControlWidth  = newControlWidth  * ZoomRatio;
      try
      {
        if (IsShutDownStarted) return;
        _dispatcher.Invoke(Zoom);
      }
      catch (TaskCanceledException) { }
    }

    private void Zoom()
    {
      var maxWidthScale  = ControlWidth / ImageSourceWidth;
      var maxHeightScale = ControlHeight / ImageSourceHeight;

      ImageSourceScale = Math.Min(maxHeightScale, maxWidthScale);
    }

    public void UpdateImageSource(BitmapImage source)
    {
      if (source == null)
        return;

      try
      {
        if (IsShutDownStarted) return;
        ImageSource = source;
        Zoom();
      }
      catch (TaskCanceledException) { }
      catch (Exception ex)
      {
        _logger.Error(ex);
      }
    }
    
    public void ShowTopLayer(bool flag)
    {
      IsTopLayerVisible = flag;
    }

    private bool _isShutDownStarted;
    private bool IsShutDownStarted => _isShutDownStarted || _dispatcher.Thread.ThreadState == ThreadState.Stopped
                                                         || _dispatcher.HasShutdownStarted
                                                         || _dispatcher.HasShutdownFinished;

    public bool HasImage => _imageSource != null && !Equals(_imageSource, _defaultImage);
    
    private object _topLayer;
    public object TopLayer
    {
      get { return _topLayer; }
      set
      {
        if (_topLayer == value) return;
        _topLayer = value;
        NotifyOfPropertyChange(() => TopLayer);
      }
    }

    private bool _isTopLayerVisible;
    public bool IsTopLayerVisible
    {
      get { return _isTopLayerVisible; }
      set
      {
        _isTopLayerVisible = value;
        NotifyOfPropertyChange(() => IsTopLayerVisible);
      }
    }

    private Bitmap _bitmap;
    public Bitmap Bitmap => _bitmap ?? 
                           (_bitmap = BitmapConversion.BitmapSourceToBitmap(ImageSource));


    private BitmapSource _imageSource;
    public BitmapSource ImageSource
    {
      get { return _imageSource ?? _defaultImage; }
      set
      {
        try
        {
          if (Equals(_imageSource, value)) return;

          _bitmap = null;

          _imageSource = BitmapUtils.ToDefaultDpi(value);
          _imageSource?.Freeze();
          if (IsShutDownStarted) return;

          NotifyOfPropertyChange(() => ImageSource);
          NotifyOfPropertyChange(() => ImageSourceWidth);
          NotifyOfPropertyChange(() => ImageSourceHeight);
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
          _logger.Error(ex);
        }
      }
    }


    //TODO rename to Width, Height
    public double ImageSourceWidth => ImageSource.Width;
    public double ImageSourceHeight => ImageSource.Height;

    //TODO rename to scale
    private double _imageSourceScale;
    public double ImageSourceScale
    {
      get { return _imageSourceScale; }
      set
      {
        if (_imageSourceScale.Equals(value)) return;
        _imageSourceScale = value;
        NotifyOfPropertyChange(() => ImageSourceScale);
      }
    }

    private double _controlWidth;
    public double ControlWidth
    {
      get { return _controlWidth; }
      set
      {
        if (!_controlWidth.Equals(value) && !value.Equals(double.NaN))
          _controlWidth = value;
        NotifyOfPropertyChange(() => ControlWidth);
      }
    }

    private double _controlHeight;
    public double ControlHeight {
      get { return _controlHeight; }
      set
      {
        if (!_controlHeight.Equals(value) && !value.Equals(double.NaN))
          _controlHeight = value;
        NotifyOfPropertyChange(() => ControlHeight);
      }
    }

    private readonly BitmapSource _defaultImage = null;

    public BitmapUtils BitmapUtils;

    private const float  ZoomRatio = 0.9f;

    private  Dispatcher _dispatcher;
    private  ILogger    _logger    ;
  }
}
