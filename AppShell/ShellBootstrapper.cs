using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using AppShell.ViewModels;
using Caliburn.Micro;

namespace AppShell
{
  //starter
  public class ShellBootstrapper : BootstrapperBase
  {
    public ShellBootstrapper()
    {
      Initialize();
    }

    protected override void Configure()
    {
      Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    protected override object GetInstance(Type service, string key)
    {
      var container = _appLauncher.Container;
      return string.IsNullOrWhiteSpace(key)

          ? container.Kernel.HasComponent(service)
              ? container.Resolve(service)
              : base.GetInstance(service, key)

          : container.Kernel.HasComponent(key)
              ? container.Resolve(key, service)
              : base.GetInstance(service, key);
    }


    protected override void OnStartup(object sender, StartupEventArgs e)
    {
      _appLauncher = new AppLauncher();

      Display();

      _appLauncher.TryStart();
    }

    protected override void OnExit(object sender, EventArgs e)
    {
      _appLauncher.TryStop(); 
    }

    private void Display()
    {
      try
      {
        DisplayRootViewFor<ShellViewModel>();
        RegisterMainWindow();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
      }
    }

    private void RegisterMainWindow()
    {
      _appLauncher.Container.Register(Castle.MicroKernel.Registration.Component.For<Window>()
                                                                   .Instance(Application.Current.MainWindow));
    }


    private AppLauncher _appLauncher;
  }
}
