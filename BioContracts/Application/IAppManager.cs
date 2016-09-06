namespace BioContracts.Application
{
  public interface IStartable
  {
    void Start();
    void Stop();
  }
  public interface IAppManager : IStartable {}
}
