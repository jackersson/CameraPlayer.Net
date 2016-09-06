using BioContracts.Application;
using Castle.Windsor;

namespace ApplicationEngine.StartApp
{
  public class ApplicationManager : IAppManager
  {
    public ApplicationManager(IWindsorContainer container)
    {
      _container = container;
    }

    public void Start()
    {
    
    }

    public void Stop()
    {
     
    }

   
    private readonly IWindsorContainer _container;
   
  }
}
