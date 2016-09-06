using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Utils.Observers
{
  public class BaseObservable<T> : IObservable<T> where T : class
  {
    public void Subscribe(T observer)
    {
      var tsk = Task.Run(() => {
        Observers.TryAdd(observer.GetHashCode(), observer);
      });
      tsk.Wait();
    }

    public void Unsubscribe(T observer)
    {
      var tsk = Task.Run(() => {
        T removed;
        Observers.TryRemove(observer.GetHashCode(), out removed);
      });
      tsk.Wait();
    }

    public void UnsubscribeAll()
    {
      var tsk = Task.Run(() =>
      {
        foreach (var observer in Observers)
        {
          T removed;
          Observers.TryRemove(observer.Value.GetHashCode(), out removed);
        }
      });
      tsk.Wait();
    }

    public bool HasObserver(T observer)
    {
      return Observers.ContainsKey(observer.GetHashCode());
    }

    public ConcurrentDictionary<int, T> Observers { get; } = new ConcurrentDictionary<int, T>();
  }
}
