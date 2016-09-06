namespace BioContracts.Devices
{
  public interface IDeviceStateObserver
  {
    void OnError(DeviceException exception);

    void OnState(IDeviceState state);
  }
  public interface IDeviceObserver<in T> : IDeviceStateObserver
  {
    void OnNext(T data);
  }

  public interface IDeviceState
  {
    DeviceState State { get; }
    DeviceType  Type  { get; }
  }

  public enum DeviceType
  {
      None
    , Card
    , Facial
    , Fingerprint
    , Iris
  }

  public enum DeviceState
  {
     None
   , Active
   , StoppedByUser
   , Paused
   , Stopped
   , Error
  }
}
