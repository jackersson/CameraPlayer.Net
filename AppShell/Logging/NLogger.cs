using BioContracts.Logging;

namespace AppShell.Logging
{
  public class NLogger : ILogger
  {
    public NLogger()
    {
      try
      {
        _logger = NLog.LogManager.GetCurrentClassLogger();
      }
      catch (NLog.NLogConfigurationException) { }
    }
    void ILogger.Debug<T>(T value)
    {
      _logger?.Debug(value);
    }

    void ILogger.Error<T>(T value)
    {
      _logger?.Error(value);
    }

    void ILogger.Fatal<T>(T value)
    {
      _logger?.Fatal(value);
    }

    void ILogger.Info<T>(T value)
    {
      _logger?.Info(value);
    }

    void ILogger.Trace<T>(T value)
    {
      _logger?.Trace(value);
    }

    void ILogger.Warn<T>(T value)
    {
      _logger?.Warn(value);
    }

    private readonly NLog.Logger _logger;
  }
}
