using BioContracts.Common;
using Utils;

namespace BioContracts.Devices
{
  public interface IDeviceEnumerator : IStartable, IStopable
  {
    bool Connected(string deviceName);
    AsyncObservableCollection<string> Devices { get; }
  }
}
