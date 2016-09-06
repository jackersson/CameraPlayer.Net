using System;
using System.Reflection;
using BioContracts.Castle;
using BioContracts.Logging;
using Caliburn.Micro;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace AppShell.Container
{
  public class ModulesLoader
  {
    public ModulesLoader(IWindsorContainer container)
    {
      _container = container;
      _logger = container.Resolve<ILogger>();
    }

    public IModule Load(Assembly assembly)
    {
      try
      {
        var moduleInstaller = FromAssembly.Instance(assembly);

        var modulecontainer = new WindsorContainer();
        _container.AddChildContainer(modulecontainer);

        modulecontainer.Install(moduleInstaller);

        var module = modulecontainer.Resolve<IModule>();

        //If ViewModel can't find View, we need to add loaded assemblies
        if (!AssemblySource.Instance.Contains(assembly))
          AssemblySource.Instance.Add(assembly);

        return module;
      }
      catch (Exception exception)
      {
        _logger.Error(exception);
        return null;
      }
    }

    private readonly IWindsorContainer _container;
    private readonly ILogger           _logger   ;
  }
}
