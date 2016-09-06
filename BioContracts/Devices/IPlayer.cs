namespace BioContracts.Devices
{
  public interface IPlayer
  {
    void TryKill();
    void TryStop();
    void TryStart();
    void TryPause();
    void TryResume();
  }
}
