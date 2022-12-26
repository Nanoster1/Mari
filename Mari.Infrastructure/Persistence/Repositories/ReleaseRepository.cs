using LinqSpecs;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.Enums;
using Mari.Domain.Releases.ValueObjects;
using Mari.Infrastructure.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace Mari.Infrastructure.Persistence.Repositories;

public class ReleaseRepository : Repository<Release, ReleaseId>, IReleaseRepository
{
    public ReleaseRepository(MariDbContext context) : base(context)
    {
    }

    public IAsyncEnumerable<Platform> GetAllPlatforms()
    {
        return Context.Set<Platform>()
            .AsAsyncEnumerable();
    }

    public async Task<Platform?> GetPlatformByName(PlatformName name, CancellationToken token = default)
    {
        return await Context.Set<Platform>()
            .Where(p => p.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task<ReleaseVersion?> GetMaxVersion(
        Specification<Release> specification,
        CancellationToken token = default)
    {
        return await Set.AsNoTracking()
            .Where(specification)
            .OrderByDescending(r => r.Version.Major)
            .ThenBy(r => r.Version.Minor)
            .ThenBy(r => r.Version.Patch)
            .Select(r => r.Version)
            .FirstOrDefaultAsync();
    }

    public IAsyncEnumerable<Release> GetCurrentReleases(Range range)
    {
        var query = Set.AsNoTracking()
            .Where(r1 =>
                r1.Status == ReleaseStatus.Complete &&
                r1.UpdateDate == Set
                    .Where(r2 => r2.Platform == r1.Platform && r2.Status == r1.Status)
                    .Max(r => r.UpdateDate));

        query = AddRange(query, range);

        return query.AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Release> GetObsoleteReleases(Range range)
    {
        var query = Set.AsNoTracking()
            .Where(r1 =>
                r1.Status == ReleaseStatus.Complete &&
                r1.UpdateDate != Set
                    .Where(r2 => r2.Platform == r1.Platform && r2.Status == r1.Status)
                    .Max(r => r.UpdateDate));

        query = AddRange(query, range);

        return query.AsAsyncEnumerable();
    }
}
