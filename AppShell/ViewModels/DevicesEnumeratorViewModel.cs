using System;
using System.Linq;
using BioContracts.Devices;
using BioContracts.Logging;
using Caliburn.Micro;
using Castle.Windsor;
using Utils;

namespace AppShell.ViewModels
{
  public delegate void DeviceChangedEventHandler(string deviceName);

  public class DevicesEnumeratorViewModel : Screen
  {
    public DevicesEnumeratorViewModel(IWindsorContainer container)
    {
      TryResolveDependencies(container);
    }

    private void TryResolveDependencies(IWindsorContainer container)
    {
      try
      {
        var devicesContainer = container.Resolve<IDevicesContainer>();
        _deviceEnumerator = devicesContainer.DevicesEnumerator;
        Devices.CollectionChanged += OnDevicesChanged;
      }
      catch (Exception exception)
      {
        _logger?.Error(exception);
      }
    }
    
    private void OnDevicesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      NotifyOfPropertyChange(() => DevicesCount);
      NotifyOfPropertyChange(() => Connected);

      if (string.IsNullOrEmpty(SelectedDevice) && Devices?.Count > 0)
        SelectedDevice = Devices.FirstOrDefault();
    }

    public void Apply()
    {
      // apply device filter
    }

    private string _selectedDevice;
    public string SelectedDevice
    {
      get { return _selectedDevice; }
      set
      {
        if (_selectedDevice == value) return;

        PreviousDevice = _selectedDevice;
        _selectedDevice = value;

        OnDeviceChanged();

        NotifyOfPropertyChange(() => SelectedDevice);
        NotifyOfPropertyChange(() => Connected);
      }
    }

    public string PreviousDevice { get; set; }

    public bool Connected => _deviceEnumerator.Connected(SelectedDevice);

    public string DevicesCount => $"({Devices?.Count})";

    private void OnDeviceChanged()
    {
      DeviceChanged?.Invoke(SelectedDevice);
    }

    public event DeviceChangedEventHandler DeviceChanged;
    public AsyncObservableCollection<string> Devices => _deviceEnumerator.Devices;


    private ILogger _logger;
    private IDeviceEnumerator _deviceEnumerator;
  }
}
