using System;

namespace Utils
{
  public class Flags<T> 
  {
    public bool Has(int value, T target, bool shift = false)
    {
      var converted = shift ? Convert.ToInt32(target) : 1 << Convert.ToInt32(target);
      return Has(value, converted);
    }

    public int Set(int value, T flag)
    {
      var converted = Convert.ToInt32(flag);
      return Set(value, converted);
    }

    public int UnSet(int value, T flag)
    {
      var converted = Convert.ToInt32(flag);
      return UnSet(value, converted);
    }

    private static int Set(int value, int flag)
    {
      return value | flag;
    }

    private static int UnSet(int value, int flag)
    {
      return value & ~flag;
    }

    private static bool Has(int value, int target)
    {
      return (value & target) == target;
    }
  }
}
