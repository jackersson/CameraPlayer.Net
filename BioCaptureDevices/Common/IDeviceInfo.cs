namespace BioCaptureDevices.Common
{
  public interface IDeviceInfo<T>
  {
    bool TryGetDeviceInfo(string deviceName, out T info);
  }
}
