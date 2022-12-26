using System.Diagnostics.CodeAnalysis;
using Mari.Domain.Common.Interfaces;

namespace Mari.Domain.Common.Models;

public class EntityComparer : IEqualityComparer<IEntity>
{
    public bool Equals(IEntity? x, IEntity? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null && y is not null) return false;
        return x!.Equals(y);
    }

    public int GetHashCode([DisallowNull] IEntity obj)
    {
        return obj.GetHashCode();
    }
}
