using AppShell.ViewModels;
using BioContracts.Castle;
using Castle.Windsor;

namespace AppShell.Container
{
  public class ShellImpl : IShell
  {
    public ShellImpl(IWindsorContainer container)
    {
      _shellViewModel = container.Resolve<ShellViewModel>();
    }
   
    private readonly ShellViewModel _shellViewModel;
  }
}
