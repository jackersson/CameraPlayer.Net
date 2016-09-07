using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using BioContracts.Devices.WebCamera;
using BioContracts.Logging;
using BioContracts.Stream;

namespace BioCaptureDevices
{
  public class WebCameraInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      try
      {
        _logger = container.Resolve<ILogger>();
        container.Register(Component.For<IWebCameraEngine>().ImplementedBy<WebCameraEngine>());
      }
      catch (Exception ex) {
        _logger?.Error(ex);
      }
    }

    private ILogger _logger;
  }
}
