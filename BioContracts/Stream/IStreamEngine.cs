using System.Collections.Generic;
using BioContracts.Castle;
using BioContracts.Devices;

namespace BioContracts.Stream
{
  public interface IStreamEngine : IModule
  {
    void Add(string deviceName);
    void Remove(string deviceName);
    void RemoveAll();
    void AddRange(ICollection<string> devices);
    IMediaPlayer GetPlayer(string deviceName); 
    IDeviceEnumerator DeviceEnumerator { get; }
    bool IsActive(string deviceName);
    void Subscribe(IDeviceObserver<IStreamData> observer, string deviceName);
    void Unsubscribe(IDeviceObserver<IStreamData> observer);
    bool HasObserver(IDeviceObserver<IStreamData> observer, string deviceName);
  }
}
