using System;

namespace Utils
{
  public class MethodInvoker
  {
    public object Invoke(Type objectType, string methodName, object source, object[] args = null)
    {
      if (source == null)
        return null;

      var method = objectType.GetMethod(methodName);
      return method?.Invoke(source, args);
    }

    public object Invoke<T>(string methodName, object source, object[] args = null)
    {
      if (source == null)
        return null;

      var objectType = typeof (T);
      var method = objectType.GetMethod(methodName);
      return method?.Invoke(source, args);
    }
  }

  public class SpecificMethodInvoker
  {
    public SpecificMethodInvoker(string methodName)
    {
      _methodName = methodName;
      _invoker = new MethodInvoker();
    }

    public object Invoke(Type objectType, object source, object[] args = null)
    {
      return _invoker.Invoke(objectType, _methodName, source, args);
    }

    public object Invoke<T>(object source, object[] args = null)
    {
      var objectType = typeof(T);
      return _invoker.Invoke(objectType, _methodName, source, args);
    }

    private readonly MethodInvoker _invoker;
    private readonly string _methodName;
  }
}
