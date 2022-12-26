namespace Mari.Domain.Common.Models;

public abstract record ValueObjectWrapper<TBase, TWrapper> :
    ValueObject,
    IComparable<ValueObjectWrapper<TBase, TWrapper>>,
    IEquatable<TBase>,
    IComparable<TBase>
    where TBase : IComparable<TBase>
    where TWrapper : ValueObjectWrapper<TBase, TWrapper>, new()
{
    protected const string PublicConstructorObsoleteMessage = "Use factory method instead";

    [Obsolete(PublicConstructorObsoleteMessage, true)]
    protected ValueObjectWrapper() { }

    public static TWrapper Create(TBase value)
    {
        var instance = new TWrapper();
        instance.OnCreate(ref value);
        instance.SetValue(value);
        return instance;
    }

    private TBase _value = default!;
    public TBase Value { get => _value; init => _value = value; }
    private void SetValue(TBase value) => _value = value;

    public virtual void OnCreate(ref TBase value) { }

    public sealed override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }

    public int CompareTo(ValueObjectWrapper<TBase, TWrapper>? other)
    {
        return Value.CompareTo(other == null ? default : other.Value);
    }

    public bool Equals(TBase? other)
    {
        return Value.Equals(other);
    }

    public int CompareTo(TBase? other)
    {
        return Value.CompareTo(other);
    }

    #region Operators
    public static implicit operator TBase(ValueObjectWrapper<TBase, TWrapper> value) => value.Value;
    public static bool operator <(ValueObjectWrapper<TBase, TWrapper> left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) < 0;
    public static bool operator >(ValueObjectWrapper<TBase, TWrapper> left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) > 0;
    public static bool operator <=(ValueObjectWrapper<TBase, TWrapper> left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) <= 0;
    public static bool operator >=(ValueObjectWrapper<TBase, TWrapper> left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) >= 0;
    public static bool operator <(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) < 0;
    public static bool operator >(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) > 0;
    public static bool operator <=(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) <= 0;
    public static bool operator >=(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) >= 0;
    public static bool operator <(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) < 0;
    public static bool operator >(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) > 0;
    public static bool operator <=(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) <= 0;
    public static bool operator >=(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) >= 0;
    public static bool operator ==(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) == 0;
    public static bool operator !=(ValueObjectWrapper<TBase, TWrapper> left, TBase right) => left.CompareTo(right) != 0;
    public static bool operator ==(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) == 0;
    public static bool operator !=(TBase left, ValueObjectWrapper<TBase, TWrapper> right) => left.CompareTo(right) != 0;
    #endregion
}
