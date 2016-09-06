namespace BioContracts.Castle
{
  public interface IShell
  {
    object FlyoutControl { get; set; }
    object TabControl { get; set; }
    object ToolBar { get; set; }
    object MainMenu { get; set; }
    object LoginInformation { get; set; }
    object StatusControl { get; set; }
  }
}
