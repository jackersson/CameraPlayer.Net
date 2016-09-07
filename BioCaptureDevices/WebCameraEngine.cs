using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using BioContracts.Devices;
using BioContracts.Devices.WebCamera;
using BioContracts.Stream;

namespace BioCaptureDevices
{
  public class WebCameraEngine : IWebCameraEngine
  {
    public WebCameraEngine()
    {
      _devices = new ConcurrentDictionary<string, WebCameraListener>(); 
        
      _deviceEnumerator = new WebCameraEnumerator();
      _deviceEnumerator.Start();
    }

    public void Add(string deviceName)
    {
      if (string.IsNullOrEmpty(deviceName))
        return;

      WebCameraListener listener;
      if (_devices.TryGetValue(deviceName, out listener))
        return;
      listener = new WebCameraListener(deviceName, _deviceEnumerator);
      listener.Start();
      _devices.TryAdd(deviceName, listener);
    }    

    public void Remove(string deviceName)
    {
      if (string.IsNullOrEmpty(deviceName))
        return;

      WebCameraListener listener;
      _devices.TryRemove(deviceName, out listener);
      listener?.TryKill();
    }

    public bool IsActive(string deviceName)
    {
      if (string.IsNullOrEmpty(deviceName))
        return false;

      WebCameraListener listener;
      return _devices.TryGetValue(deviceName, out listener) && listener.IsActive();
    }
    
    public void Subscribe(IDeviceObserver<IStreamData> observer, string deviceName) 
    {
      if (observer == null || string.IsNullOrEmpty(deviceName))
        return;

      WebCameraListener listener;
      if (_devices.TryGetValue(deviceName, out listener))      
        listener.Subscribe(observer);      
    }

    public void Unsubscribe(IDeviceObserver<IStreamData> observer)
    {
      if (observer == null)
        return;

      foreach (var listener in _devices.Select(par => par.Value)
                                       .Where(listener => listener.HasObserver(observer)))
      listener.Unsubscribe(observer);
      
    }

    public bool HasObserver(IDeviceObserver<IStreamData> observer, string deviceName)
    {
      if (string.IsNullOrEmpty(deviceName))
        return false;

      WebCameraListener listener;
      return _devices.TryGetValue(deviceName, out listener) && listener.HasObserver(observer);
    }

    public void RemoveAll()
    {
      _deviceEnumerator.Stop();

      foreach (var par in _devices)
        par.Value.TryKill();

      _devices.Clear();
    }

    public void AddRange(ICollection<string> devices)
    {
      if (devices == null || devices.Count <= 0)
      {
        RemoveAll();
        return;
      }

      var devicesToAdd    = devices.Where      (x => !ContainsKey(x));
      var devicesToRemove = _devices.Keys.Where(x => !devices.Contains(x)   );

      foreach (var deviceName in devicesToAdd.Where(deviceName => !string.IsNullOrEmpty(deviceName)))
        Add(deviceName);

      foreach (var deviceName in devicesToRemove.Where(deviceName => !string.IsNullOrEmpty(deviceName)))
        Remove(deviceName);
    }

    public IMediaPlayer GetPlayer(string deviceName)
    {
      if (string.IsNullOrEmpty(deviceName))
        return null;

      WebCameraListener listener;
      return _devices.TryGetValue(deviceName, out listener) ? listener : null;
    }
    
    public void DeInit()
    {
      RemoveAll();
      DeviceEnumerator.Stop();
    }

    public void Init()
    {
      DeviceEnumerator.Start();
    }
    private bool ContainsKey(string key)
    {
      WebCameraListener result;
      return _devices.TryGetValue(key, out result);
    }

    public IDeviceEnumerator DeviceEnumerator => _deviceEnumerator;

    private readonly WebCameraEnumerator _deviceEnumerator;
    private readonly ConcurrentDictionary<string, WebCameraListener> _devices;
  }
}
