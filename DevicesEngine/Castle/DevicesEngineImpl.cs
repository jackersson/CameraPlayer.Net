using System;
using BioContracts.Castle;
using BioContracts.Devices;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DevicesEngine.Castle
{
  public class DevicesEngineImpl : IModule
  {

    public DevicesEngineImpl(IWindsorContainer container)
    {
      _container = container;
    }

    public void DeInit()
    {
      throw new NotImplementedException();
    }

    public void Init()
    {
      _container.Register(Component.For<IDevicesContainer>().ImplementedBy<DevicesContainer>());
    }

    private readonly IWindsorContainer _container;
  }
}
