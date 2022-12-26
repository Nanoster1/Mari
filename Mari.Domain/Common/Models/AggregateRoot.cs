using Mari.Domain.Common.Interfaces;

namespace Mari.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : IEquatable<TId>, IHasDefaultValue<TId>
{
}
