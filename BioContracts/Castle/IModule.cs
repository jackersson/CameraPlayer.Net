using System.Threading.Tasks;

namespace BioContracts.Castle
{
  public interface IModule
  {
    void Init();

    void DeInit();
  }

  public interface IAsyncModule
  {
    Task Init();

    Task DeInit();
  }
}
