using System.Collections.Generic;
using BioContracts.Devices;
using BioContracts.Devices.WebCamera;
using Castle.Components.DictionaryAdapter;
using Castle.Windsor;
using Utils;

namespace DevicesEngine
{
  public class DevicesEnumerator : IDeviceEnumerator
  {
    public DevicesEnumerator( IWindsorContainer container )
    {
      var webCameraEnumerator = container.Resolve<IWebCameraEngine>().DeviceEnumerator;
      _enumerators.Add(webCameraEnumerator);


      //TODO remporal
      Devices = webCameraEnumerator.Devices;
    }

    public void Start()
    {
      foreach (var enumerator in _enumerators)
        enumerator.Start();
    }

    public void Stop()
    {
      foreach (var enumerator in _enumerators)
        enumerator.Stop();
    }

    public bool Connected(string deviceName) {
      return Devices.Contains(deviceName);
    }

    public AsyncObservableCollection<string> Devices { get; set; }

    public void Apply(DeviceType flag)
    {
      _mode = _flags.Set(_mode, flag);
    }


    private int _mode = (int)DeviceType.Facial;
    private readonly List<IDeviceEnumerator> _enumerators = new EditableList<IDeviceEnumerator>();
    private readonly Flags<DeviceType> _flags = new Flags<DeviceType>();

  }
}
