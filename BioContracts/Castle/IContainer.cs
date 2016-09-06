namespace BioContracts.Castle
{
  public interface IContainer
  {
    T Resolve<T>();
  }
}
