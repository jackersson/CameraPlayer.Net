using BioContracts.Castle;
using BioContracts.Common;

namespace BioContracts.Devices
{
  public interface IDevicesContainer : IContainer, ILifecycle
  {
    IDeviceEnumerator DevicesEnumerator { get; }
  }
}
