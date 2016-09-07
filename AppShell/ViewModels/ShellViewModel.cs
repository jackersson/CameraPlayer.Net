using System;
using System.Drawing;
using System.Windows.Media;
using BioContracts.Devices;
using BioContracts.Devices.WebCamera;
using BioContracts.Logging;
using BioContracts.Stream;
using Caliburn.Micro;
using Castle.Windsor;
using ImageViewer.ViewModels;
using Utils;

namespace AppShell.ViewModels
{
  public sealed class ShellViewModel : Screen, IDeviceObserver<IStreamData>
  {
    public ShellViewModel(IWindsorContainer container)
    {
      DisplayName = "Main window";

      _container = container;
      TryResolveDependencies(container);
    }

    private void TryResolveDependencies(IWindsorContainer container)
    {
      try
      {
        _logger = container.Resolve<ILogger>();

        _devicesContainer = container.Resolve<IDevicesContainer>();

        Devices = new DevicesEnumeratorViewModel(container);
        Devices.DeviceChanged += OnDeviceChanged;

        Image = new ImageItemViewModel(container) {ControlWidth = 500, ControlHeight = 500};
      }
      catch (Exception exception) {
        _logger?.Error(exception);
      }

    }

    private void OnDeviceChanged(string deviceName)
    {
      var engine = _devicesContainer.Resolve<IWebCameraEngine>();
      engine.Add(deviceName);
      engine.Subscribe(this, deviceName);
      Player = new PlayerViewModel(engine.GetPlayer(deviceName));
    }

    private PlayerViewModel _player;
    public PlayerViewModel Player
    {
      get { return _player; }
      set
      {
        if (value != null && _player != value)
          _player = value;

        NotifyOfPropertyChange(() => Player);
      }
    }

    public ImageItemViewModel Image { get; set; }

    private DevicesEnumeratorViewModel _devices;
    public DevicesEnumeratorViewModel Devices
    {
      get { return _devices; }
      set
      {
        if (value != null && _devices != value)
          _devices = value;

        NotifyOfPropertyChange(() => Devices);
      }
    }

    private ILogger           _logger;
    private readonly IWindsorContainer _container;
    private IDevicesContainer _devicesContainer;

    public void OnNext(IStreamData data)
    {
      Bitmap image;
      if (data.TryGetBitmap(StreamType.StreamTypeColor, out image))
        Image.UpdateImageSource(BitmapConversion.BitmapToBitmapImage(image));
    }

    public void OnError(DeviceException exception)
    {
      _logger?.Error(exception);
    }

    public void OnState(IDeviceState state)
    {
      _logger?.Trace("Webcamera state " + state.State);
    }
  }
}
