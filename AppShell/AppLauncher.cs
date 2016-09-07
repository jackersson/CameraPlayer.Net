using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AppShell.Container;
using BioContracts.Application;
using BioContracts.Castle;
using BioContracts.Logging;
using Castle.Core.Internal;
using Castle.Windsor;

namespace AppShell
{
  public class AppLauncher
  {
    public AppLauncher(string exeDirectory = "")
    {
      IsOk = LoadDependencies(exeDirectory);
    }

    public void TryStop()
    {
      try
      {
        Container.Resolve<IAppManager>().Stop();
        _modules.ForEach(m =>
        {
          try
          {
            m.DeInit();
          }
          catch (Exception exception)
          {
            _logger.Error(exception);
          }
        });
        _logger.Info("Application stoped ...");
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
      }
    }

    public void TryStart()
    {
      try
      {
        Container.Resolve<IAppManager>().Start();
        _logger.Info("Application started ...");
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
      }
    }


    private bool LoadDependencies(string exeDirectory = "")
    {
      try
      {
        Container.Install(new ShellInstaller());
        _logger = Container.Resolve<ILogger>();

        var loader = new ModulesLoader(Container);

        if (string.IsNullOrEmpty(exeDirectory))
          exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!string.IsNullOrEmpty(exeDirectory))
        {
          string[] files = { "DevicesEngine.dll", "ApplicationEngine.dll" };

          files
            .Select(Assembly.LoadFrom)
            .Select(loader.Load)
            .Where(module => module != null)
            .ForEach(module => { module.Init(); _modules.Add(module); });
        }

        _logger.Info("Dependencies loaded ...");
        return true;
      }
      catch (Exception ex)
      {
        _logger.Error(ex);
        return false;
      }
    }

    public bool IsOk { get; private set; }

    public IWindsorContainer Container { get; } = new WindsorContainer();

    private readonly List<IModule> _modules = new List<IModule>();

    private ILogger _logger;
  }
}
