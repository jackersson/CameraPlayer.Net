using AppShell.ViewModels;
using BioContracts.Castle;

namespace AppShell.Container
{
  public class ShellImpl : IShell
  {
    public ShellImpl(ShellViewModel shellViewModel) {
      _shellViewModel = shellViewModel;
    }

    public object TabControl
    {
      get { return _shellViewModel.TabControl; }
      set
      {
        _shellViewModel.TabControl = value;
      }
    }

    public object FlyoutControl
    {
      get { return _shellViewModel.FlyoutControl; }
      set
      {
        _shellViewModel.FlyoutControl = value;
      }
    }

    public object ToolBar
    {
      get { return _shellViewModel.ToolBar; }
      set
      {
        _shellViewModel.ToolBar = value;
      }
    }

    public object MainMenu
    {
      get { return _shellViewModel.MainMenu; }
      set
      {
        _shellViewModel.MainMenu = value;
      }
    }
    public object LoginInformation
    {
      get { return _shellViewModel.LoginInformation; }
      set
      {
        _shellViewModel.LoginInformation = value;
      }
    }

    public object StatusControl
    {
      get { return _shellViewModel.StatusControl; }
      set
      {
        _shellViewModel.StatusControl = value;
      }
    }

    private readonly ShellViewModel _shellViewModel;
  }
}
