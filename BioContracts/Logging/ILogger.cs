namespace BioContracts.Logging
{
  public interface ILogger
  {
    /// <summary>
    /// Executed queries, user authenticated, session expired
    /// </summary>
    void Debug<T>(T value);

    /// <summary>
    /// Normal behavior like mail sent, user updated profile etc.
    /// </summary>
    void Info<T>(T value);

    /// <summary>
    /// Incorrect behavior but the application can continue
    /// </summary>
    void Warn<T>(T value);


    /// <summary>
    /// For example application crashes / exceptions.
    /// </summary>
    void Error<T>(T value);

    /// <summary>
    /// Highest level: important stuff down
    /// </summary>
    void Fatal<T>(T value);

    /// <summary>
    /// Begin method X, end method X etc
    /// </summary>
    void Trace<T>(T value);
  }
}
