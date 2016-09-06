using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using BioContracts.Logging;
using BioContracts.Stream;

namespace BioCaptureDevices
{
  public class BioCaptureDevicesInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      try
      {
        _logger = container.Resolve<ILogger>();
        container.Register(Component.For<IStreamEngine>().ImplementedBy<CaptureDeviceEngine>());
      }
      catch (Exception ex) {
        _logger?.Error(ex);
      }
    }

    private ILogger _logger;
  }
}
