using Caliburn.Micro;

namespace AppShell.ViewModels
{
  public sealed class ShellViewModel : Screen
  {
    public ShellViewModel()
    {
      DisplayName = "Main window";
    }

    private object _mainMenu;
    public object MainMenu
    {
      get { return _mainMenu; }
      set
      {
        if (_mainMenu == value) return;
        _mainMenu = value;
        NotifyOfPropertyChange(() => MainMenu);
      }
    }

    private object _toolBar;
    public object ToolBar
    {
      get { return _toolBar; }
      set
      {
        if (_toolBar == value) return;
        _toolBar = value;
        NotifyOfPropertyChange(() => ToolBar);
      }
    }

    private object _tabControl;
    public object TabControl
    {
      get { return _tabControl; }
      set
      {
        if (_tabControl == value) return;
        _tabControl = value;
        NotifyOfPropertyChange(() => TabControl);
      }
    }

    private object _flyoutControl;
    public object FlyoutControl
    {
      get { return _flyoutControl; }
      set
      {
        if (_flyoutControl == value) return;
          _flyoutControl = value;
        NotifyOfPropertyChange(() => FlyoutControl);
      }
    }

    private object _statusContol;
    public object StatusControl
    {
      get { return _statusContol; }
      set
      {
        if (_statusContol == value) return;
          _statusContol = value;
        NotifyOfPropertyChange(() => StatusControl);
      }
    }

    private object _loginInformation;
    public object LoginInformation
    {
      get { return _loginInformation; }
      set
      {
        if (_loginInformation == value) return;
        _loginInformation = value;
        NotifyOfPropertyChange(() => LoginInformation);
      }
    }
  }
}
