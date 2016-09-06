using System;
using System.Windows;
using System.Windows.Threading;
using AppShell.Logging;
using AppShell.ViewModels;
using BioContracts.Castle;
using BioContracts.Logging;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AppShell.Container
{
  public class ShellInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      try
      {
        container.Register(Component.For<IWindsorContainer>().Instance(container))
                 .Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>())
                 .Register(Component.For<ILogger>().ImplementedBy<NLogger>())
                 .Register(Component.For<Dispatcher>().Instance(Application.Current.Dispatcher))
                 .Register(Component.For<IShell>().ImplementedBy<ShellImpl>())
                 .Register(Component.For<ShellViewModel>());
       
        _logger = container.Resolve<ILogger>();
      }
      catch (Exception ex)
      {
        _logger?.Error(ex); 
      }
    }

    private ILogger _logger;
  }
}
