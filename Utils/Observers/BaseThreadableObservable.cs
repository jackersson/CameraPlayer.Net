using System.Collections.Concurrent;

namespace Utils.Observers
{
  public abstract class BaseThreadableObservable<T> : Threadable, IObservable<T> where T : class
  {
    public bool HasObserver(T observer)
    {
      return _oservable.HasObserver(observer);
    }

    public void Subscribe(T observer)
    {
      _oservable.Subscribe(observer);
    }

    public void Unsubscribe(T observer)
    {
      _oservable.Unsubscribe(observer);
    }

    public void UnsubscribeAll()
    {
      _oservable.UnsubscribeAll();
    }

    protected ConcurrentDictionary<int, T> Observers => _oservable.Observers;
    private readonly BaseObservable<T> _oservable = new BaseObservable<T>();
  }
}
