using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace Utils
{
  public class AsyncObservableCollection<T> : ObservableCollection<T>
  {
    public AsyncObservableCollection(){}
    public AsyncObservableCollection(IEnumerable<T> list): base(list){}

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (SynchronizationContext.Current == _synchronizationContext)
        RaiseCollectionChanged(e);
      else
        _synchronizationContext?.Send(RaiseCollectionChanged, e);
    }

    private void RaiseCollectionChanged(object param)
    {
      try
      {
        base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
      }
      catch (Exception)
      {
        // ignored
      }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (SynchronizationContext.Current == _synchronizationContext)
        RaisePropertyChanged(e);
      else
        _synchronizationContext?.Send(RaisePropertyChanged, e);
    }

    private void RaisePropertyChanged(object param){
      base.OnPropertyChanged((PropertyChangedEventArgs)param);
    }

    private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
  }
}
