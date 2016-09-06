using System;
using ApplicationEngine.StartApp;
using BioContracts.Application;
using BioContracts.Castle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ApplicationEngine
{
  public class ApplicationEngineImpl : IModule
  {
    public ApplicationEngineImpl(IWindsorContainer container)
    {
      _container = container;
    }

    public void DeInit()
    {
      throw new NotImplementedException();
    }

    public void Init()
    {
      _container.Register(Component.For<IAppManager>().ImplementedBy<ApplicationManager>());
    }

    private readonly IWindsorContainer _container;
  }
}
