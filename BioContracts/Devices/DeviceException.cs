using System;

namespace BioContracts.Devices
{
  public class DeviceException : Exception
  {
    public DeviceException(DeviceType deviceType, string message) : base(message)
    {
      DeviceType = deviceType;
    }

    public DeviceException(DeviceType deviceType, string message, Exception inner) : base(message, inner)
    {
      DeviceType = deviceType;
    }

    public DeviceType DeviceType { get; private set; }
  }
}
