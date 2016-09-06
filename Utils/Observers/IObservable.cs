
namespace Utils.Observers
{
  public interface IObservable<in T> where T : class
  {
    void Subscribe(T observer);

    void Unsubscribe(T observer);

    bool HasObserver(T observer);

    void UnsubscribeAll();
  }
}
