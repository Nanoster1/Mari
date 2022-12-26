using LinqSpecs;
using Mari.Application.Common.Interfaces.Persistence.Shared;
using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Mari.Infrastructure.Persistence.Shared;

public abstract class Repository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId>
    where TId : IEquatable<TId>, IHasDefaultValue<TId>
{
    protected readonly DbContext Context;
    protected readonly DbSet<TAggregateRoot> Set;

    protected Repository(DbContext context)
    {
        Context = context;
        Set = context.Set<TAggregateRoot>();
    }

    public virtual async Task<TAggregateRoot?> GetById(TId id, CancellationToken token = default)
    {
        return await Set.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
    }

    public IAsyncEnumerable<TAggregateRoot> GetById(IEnumerable<TId> ids, CancellationToken token = default)
    {
        ids = ids.ToList();
        return Set.Where(x => ids.Contains(x.Id))
            .AsNoTracking()
            .AsAsyncEnumerable();
    }

    public virtual async Task<TAggregateRoot?> Find(Specification<TAggregateRoot> specification, CancellationToken token = default)
    {
        return await Set.AsNoTracking()
            .FirstOrDefaultAsync(specification, token);
    }

    public virtual IAsyncEnumerable<TAggregateRoot> FindMany(
        Specification<TAggregateRoot>? specification = null,
        Range range = default)
    {
        var query = Set.Where(specification ?? new TrueSpecification<TAggregateRoot>());
        query = AddRange(query, range);
        return query.AsNoTracking().AsAsyncEnumerable();
    }

    public virtual Task<TAggregateRoot> Insert(TAggregateRoot aggregateRoot, CancellationToken token = default)
    {
        Set.Add(aggregateRoot);
        return Task.FromResult(aggregateRoot);
    }

    public virtual Task<TAggregateRoot> Update(TAggregateRoot aggregateRoot, CancellationToken token = default)
    {
        if (token.IsCancellationRequested) return Task.FromCanceled<TAggregateRoot>(token);
        Set.Update(aggregateRoot);
        return Task.FromResult(aggregateRoot);
    }

    public Task Delete(TAggregateRoot aggregateRoot, CancellationToken token = default)
    {
        if (token.IsCancellationRequested) return Task.FromCanceled(token);
        Set.Remove(aggregateRoot);
        return Task.CompletedTask;
    }

    public async Task DeleteById(TId id, CancellationToken token = default)
    {
        var aggregateRoot = await Set.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
        if (aggregateRoot is not null) Set.Remove(aggregateRoot);
    }

    public async Task<bool> Exists(TId id, CancellationToken token = default)
    {
        return await Set.AnyAsync(x => x.Id.Equals(id), token);
    }

    public virtual Task<bool> Exists(Specification<TAggregateRoot>? specification = null, CancellationToken token = default)
    {
        return Set.AnyAsync(specification ?? new TrueSpecification<TAggregateRoot>(), token);
    }

    public virtual async Task<int> Count(Specification<TAggregateRoot>? specification = null, CancellationToken token = default)
    {
        return await Set.CountAsync(specification ?? new TrueSpecification<TAggregateRoot>(), token);
    }

    protected IQueryable<TAggregateRoot> AddRange(IQueryable<TAggregateRoot> query, Range range)
    {
        if (range.Equals(Range.All) || range.Equals(default)) return query;

        if (range.Start.Value > range.End.Value)
        {
            throw new ArgumentOutOfRangeException(
                nameof(range),
                $"{nameof(range.Start)} must be less than or equal to {nameof(range.End)}.");
        }

        if (range.Start.IsFromEnd || range.End.IsFromEnd)
        {
            throw new ArgumentOutOfRangeException(
                nameof(range),
                $"{nameof(range.Start)} and {nameof(range.End)} must not be from end.");
        }

        if (range.Equals(default))
        {
            return query;
        }

        return query.Skip(range.Start.Value)
            .Take(range.End.Value - range.Start.Value);
    }
}
