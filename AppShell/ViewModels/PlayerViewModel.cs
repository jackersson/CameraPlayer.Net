using BioContracts.Stream;
using Caliburn.Micro;

namespace AppShell.ViewModels
{
  public class PlayerViewModel : PropertyChangedBase
  {
    public PlayerViewModel(IMediaPlayer mediaPlayer)
    {
      _mediaPlayer = mediaPlayer;
    }

    public void Start()
    {
      _mediaPlayer.TryStart();
    }

    public void Stop()
    {
      _mediaPlayer.TryStop();
    }

    public void Resume()
    {
      _mediaPlayer.TryResume();
    }

    public void Pause()
    {
      _mediaPlayer.TryPause();
    }

    public void Settings()
    {
      _mediaPlayer.TryPause();
    }

    public void Resolution()
    {
      _mediaPlayer.TryPause();
    }

    private readonly IMediaPlayer _mediaPlayer;
  }
}
