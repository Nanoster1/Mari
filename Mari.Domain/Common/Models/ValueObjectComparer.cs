using System.Diagnostics.CodeAnalysis;

namespace Mari.Domain.Common.Models;

public class ValueObjectComparer : IEqualityComparer<ValueObject>
{
    public bool Equals(ValueObject? x, ValueObject? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null && y is not null) return false;
        return x!.Equals(y);
    }

    public int GetHashCode([DisallowNull] ValueObject obj)
    {
        return obj.GetHashCode();
    }
}
