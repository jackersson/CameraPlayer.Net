using System;
using System.Linq;
using System.Threading;
using Accord.Video.DirectShow;
using BioCaptureDevices.Common;
using BioContracts.Devices;
using Utils;

namespace BioCaptureDevices
{
  public class WebCameraEnumerator : Threadable, IDeviceEnumerator, IDeviceInfo<FilterInfo>
  {
     public WebCameraEnumerator()
    {
      _devices = new AsyncObservableCollection<string>();
    }

    protected override void Run()
    {
      Active = true;

      while (Active)
      {
        try {
          ActualDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }
        catch (Exception) {
          //Not implemented
        }
        Thread.Sleep(DeviceEnumerationDelay);
      }
    }
    
    private void Update()
    {
      
      foreach (var deviceName in _devices.ToArray())
      {
        var exists = false;
        foreach (FilterInfo dN in _actualDevices)
        {
          if (string.Equals(deviceName, dN.Name))
          {
            exists = true;
            break;
          }             
        }
        if (!exists)
          _devices.Remove(deviceName);
      }

      if (_actualDevices.Count <= 0)
        return;
      
      foreach (FilterInfo filter in _actualDevices)
      {
        var deviceName = filter.Name;
        if (!_devices.Contains(deviceName))
          _devices.Add(deviceName);
      }
    }
    
    public bool Connected(string name)
    {
      FilterInfo info;
      return TryGetDeviceInfo(name, out info);
    }

    public bool TryGetDeviceInfo(string deviceName, out FilterInfo info)
    {
      try
      {
        foreach (var fi in _actualDevices.Cast<FilterInfo>()
          .Where(fi => string.Equals(fi.Name, deviceName)))
        {
          info = fi;
          return true;
        }
      }
      catch (Exception) {
        //Not implemented
      }

      info = null;
      return false;
    }


    private AsyncObservableCollection<string> _devices;
    public AsyncObservableCollection<string> Devices
    {
      get { return _devices; }
      set
      {
        if (_devices != value) return;
          _devices = value;
      }
    }


    private FilterInfoCollection _actualDevices;
    public FilterInfoCollection ActualDevices
    {
      get { return _actualDevices; }
      set
      {
        if (_actualDevices == value) return;
        _actualDevices = value;
        Update();
      }
    }

    private const int DeviceEnumerationDelay = 2000;
  }
}
