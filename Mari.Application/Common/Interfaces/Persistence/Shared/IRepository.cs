using LinqSpecs;
using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Application.Common.Interfaces.Persistence.Shared;

public interface IRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId>
    where TId : IEquatable<TId>, IHasDefaultValue<TId>
{
    Task<TAggregateRoot?> GetById(TId id, CancellationToken token = default);
    IAsyncEnumerable<TAggregateRoot> GetById(IEnumerable<TId> ids, CancellationToken token = default);
    Task<TAggregateRoot?> Find(Specification<TAggregateRoot> specification, CancellationToken token = default);
    IAsyncEnumerable<TAggregateRoot> FindMany(Specification<TAggregateRoot>? specification = null, Range range = default);
    Task<TAggregateRoot> Insert(TAggregateRoot aggregateRoot, CancellationToken token = default);
    Task<TAggregateRoot> Update(TAggregateRoot aggregateRoot, CancellationToken token = default);
    Task Delete(TAggregateRoot aggregateRoot, CancellationToken token = default);
    Task DeleteById(TId id, CancellationToken token = default);
    Task<bool> Exists(TId id, CancellationToken token = default);
    Task<bool> Exists(Specification<TAggregateRoot>? specification = null, CancellationToken token = default);
    Task<int> Count(Specification<TAggregateRoot>? specification = null, CancellationToken token = default);
}
