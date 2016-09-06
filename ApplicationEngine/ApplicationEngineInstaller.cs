using System;
using BioContracts.Castle;
using BioContracts.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ApplicationEngine
{
  public class ApplicationEngineInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      try
      {
        _logger = container.Resolve<ILogger>();
        container.Register(Component.For<IModule>().ImplementedBy<ApplicationEngineImpl>());
      }
      catch (Exception ex)
      {
        _logger?.Error(ex);
      }
    }

    private ILogger _logger;
  }
}
