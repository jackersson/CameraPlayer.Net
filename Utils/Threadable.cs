using System.Threading;

namespace Utils
{
  public abstract class Threadable
  {
    protected Threadable()
    {
      Active = false;
      CancellationTokenResult = new CancellationTokenSource();
    }

    public bool Active { get; set; }

    public void Start()
    {
      if ( Active )
        return;
      ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProcedure), this);
    }

    public virtual void Stop()
    {
      CancellationTokenResult.Cancel();
    }

    protected bool CancelationRequested => CancellationTokenResult.IsCancellationRequested;



    protected abstract void Run();

    protected CancellationTokenSource CancellationTokenResult { get; }

    protected void ThreadProcedure(object threadContext)
    {
      var threadable = (Threadable)threadContext;

      if (threadable == null)
        return;
      threadable.Active = true;
      threadable.Run();
      threadable.Active = false;
    }
  }
}
