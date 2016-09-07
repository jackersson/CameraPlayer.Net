using System.Collections.Specialized;

namespace ImageViewer.Contracts
{
  public interface IImageItemCollection
  {
    void AddItem   (IImageItemScreen item);
    void RemoveItem(IImageItemScreen item);
    void Subscribe  (NotifyCollectionChangedEventHandler eventHandler);
    void Unsubscribe(NotifyCollectionChangedEventHandler eventHandler);
    int Count { get; }

    void MakeActive(IImageItemScreen item);
    IImageItemScreen ActiveItem { get; }
    void Clear();
  }

}
