using BioCaptureDevices.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Accord.Video;
using Accord.Video.DirectShow;
using BioContracts.Devices;
using BioContracts.Stream;
using Utils.Observers;

namespace BioCaptureDevices
{
  public class WebCameraListener : BaseThreadableObservable<IDeviceObserver<IStreamData>>
                                     , IMediaPlayer
  { 
    public WebCameraListener( string deviceName, IDeviceInfo<FilterInfo> devices)
    {
      _deviceName         = deviceName;
      _devices            = devices;

      TryConnect();

    }

    public void TryKill()
    {
      _commands.Enqueue(CaptureDeviceCommands.Kill);
    }

    public void TryStop()
    {
      _commands.Enqueue(CaptureDeviceCommands.Stop);
    }
    public void TryStart()
    {
      _commands.Enqueue(CaptureDeviceCommands.Start);
    }

    public void TryPause()
    {
      _commands.Enqueue(CaptureDeviceCommands.Pause);
    }

    public void TryResume()
    {
      _commands.Enqueue(CaptureDeviceCommands.Resume);
    }

    private void TryConnect()
    {
      _commands.Enqueue(CaptureDeviceCommands.Connect);
    }

    public void ShowSettings(IntPtr parentWindow)
    {
      _videoSource?.DisplayPropertyPage(parentWindow);
    }

    public void SetFormat(IMediaFormat format)
    {
      var currentVideoResolution = _videoSource.VideoResolution;

      if (_utils.IsEqual(currentVideoResolution, format))
        return;


      var selected = _videoSource.VideoCapabilities
                                 .FirstOrDefault(x => _utils.IsEqual(x, format));

      if (selected == null || selected == currentVideoResolution)
        return;

      TryStop();
      _videoSource.VideoResolution = selected;
      TryStart();
    }

    public List<IMediaFormat> Formats()
    {
      return _utils.Formats(_videoSource);
    }

    private void OpenStream(FilterInfo fi)
    {    
      if (fi == null)      
        return;

      _videoSource = new VideoCaptureDevice(fi.MonikerString);  
     
      _videoSource.PlayingFinished += OnVideoSourcePlayingFinished;
      _videoSource.NewFrame        += OnFrame;

      _videoSource.VideoResolution = _utils.BestResolution(_videoSource);
   
      TryStart();
      //OnState(DeviceState.Active);
    }
    
    private void OnVideoSourcePlayingFinished(object sender, ReasonToFinishPlaying reason)
    {
      if (reason != ReasonToFinishPlaying.StoppedByUser)
      {
        _commands.Enqueue(CaptureDeviceCommands.Connect);
        var ex = new Exception(WebCameraErrorInfo.GetErrorMessage(reason));
        OnError(ex);
      }
      OnState(DeviceState.StoppedByUser);      
    }    
    
    private void OnFrame(object sender, NewFrameEventArgs eventArgs)
    {
      var streamData = new StreamData();
      streamData.AddFrame(eventArgs.Frame, StreamType.StreamTypeColor);
      OnNext(streamData);
    }

    protected override void Run()
    {     
      Active = true;


      while (Active)
      {
        CaptureDeviceCommands command;
        var result = Dequeue(out command);

        if ( result )
        {
          switch (command)
          {
            case CaptureDeviceCommands.Kill:
              DoStopStream();
              Stop();
              break;

            case CaptureDeviceCommands.Start:             
              DoStart();
              break;

            case CaptureDeviceCommands.Stop:
              DoStopStream();
              break;

            case CaptureDeviceCommands.Connect:
              {
                FilterInfo filterInfo;
                if ( _devices.TryGetDeviceInfo(_deviceName, out filterInfo) )
                  OpenStream(filterInfo);   
                else
                  TryConnect();
                break;
              }
            case CaptureDeviceCommands.Pause:
              DoPause();
              break;
            case CaptureDeviceCommands.Resume:
              DoResume();
              break;
          }
        }
        Thread.Sleep(DelayBetweenCommands);
      }
    }

    private void DoResume()
    {
      _videoSource.Start();
      OnState(DeviceState.Active);
    }

    private void DoPause()
    {
      DoStopStream();
      OnState(DeviceState.Paused);
    }

    private void DoStart()
    {
      _videoSource.Start();
      OnState(DeviceState.Active);
    }

    private bool Dequeue(out CaptureDeviceCommands command)
    {
      lock (_commands)
      {
        while (_commands.IsEmpty)
          Thread.Sleep(EmptyCommandTimeout);

        return _commands.TryDequeue(out command);
      }
    }

    private void DoStopStream()
    {
      if (null == _videoSource)
        return;

      try
      {
        _videoSource.SignalToStop();
        _videoSource.WaitForStop();

        if (_videoSource.IsRunning)
          _videoSource.Stop();

        OnState(DeviceState.Stopped);
      }
      catch (Exception ex) {
        OnError(ex);
      }
    }

    public bool IsActive()
    {
      return _videoSource != null && _videoSource.IsRunning;
    }
    private void OnNext(IStreamData streamData)
    {
      foreach (var observer in Observers)
        observer.Value.OnNext(streamData);
    }

    private void OnState(DeviceState state)
    {
      var deviceState = new CaptureDeviceState(state);
      foreach (var observer in Observers)
        observer.Value.OnState(deviceState);
    }
    
    private void OnError(Exception exception)
    {
      if (exception == null)
        return;

      var deviceException = new DeviceException(DeviceType.Facial, exception.Message, exception);
      foreach (var observer in Observers)
        observer.Value.OnError(deviceException);
    }

    private const int EmptyCommandTimeout  = 500;
    private const int DelayBetweenCommands = 500;

    private readonly Common.Utils                           _utils = new Common.Utils();
    private VideoCaptureDevice                              _videoSource       ;
    private readonly IDeviceInfo<FilterInfo>                _devices           ;
    private readonly string                                 _deviceName        ;
    private readonly ConcurrentQueue<CaptureDeviceCommands> _commands         = new ConcurrentQueue<CaptureDeviceCommands>();
  }

  internal class CaptureDeviceState : IDeviceState
  {
    public CaptureDeviceState(DeviceState state)
    {
      State = state;
    }
    public DeviceState State { get; }
    public DeviceType Type => DeviceType.Facial;
  }

  internal enum CaptureDeviceCommands
  {
      None = 0
    , Kill
    , Stop
    , Connect
    , Start
    , Pause
    , Resume
  }
}
