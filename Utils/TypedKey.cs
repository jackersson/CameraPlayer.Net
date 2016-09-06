using System;

namespace Utils
{
  public class TypedKey : IEquatable<TypedKey>
  {
    public TypedKey(Type first, Type second)
    {
      _types = new Tuple<Type, Type>(first, second);
    }

    public Type First => _types.Item1;
    public Type Second => _types.Item2;

    public override int GetHashCode()
    {
      return _types.Item1.GetHashCode() | _types.Item2.GetHashCode();
    }

    public bool Contains(Type type)
    {
      return type == First || type == Second;
    }

    public bool Equals(TypedKey other)
    {
      return (other.First == First || other.First == Second)
           && (other.Second == First || other.Second == Second);
    }

    private readonly Tuple<Type, Type> _types;

  }
}
