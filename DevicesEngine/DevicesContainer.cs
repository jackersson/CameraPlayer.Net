using BioCaptureDevices;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using BioContracts.Devices;
using BioContracts.Devices.WebCamera;

namespace DevicesEngine
{
  public class DevicesContainer : IDevicesContainer
  {
    public DevicesContainer()
    {
      _container.Register(Component.For<IWebCameraEngine>()
                                   .ImplementedBy<WebCameraEngine>());

      _enumerator = new DevicesEnumerator(_container);
    }

    public T Resolve<T>()
    {
      return _container.Resolve<T>();
    }

    public void Stop()
    {
      var engine = _container.Resolve<IWebCameraEngine>();
      engine.RemoveAll();
      _enumerator.Stop();
    }

    public void Start()
    {
      _enumerator.Start();
    }

    public IDeviceEnumerator DevicesEnumerator => _enumerator;

    private readonly IWindsorContainer _container  = new WindsorContainer ();
    private readonly DevicesEnumerator _enumerator;
  
  }


}
