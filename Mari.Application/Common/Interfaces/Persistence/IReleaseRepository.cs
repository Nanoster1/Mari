using LinqSpecs;
using Mari.Application.Common.Interfaces.Persistence.Shared;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Common.Interfaces.Persistence;

public interface IReleaseRepository : IRepository<Release, ReleaseId>
{
    IAsyncEnumerable<Platform> GetAllPlatforms();
    Task<Platform?> GetPlatformByName(PlatformName name, CancellationToken token = default);
    Task<ReleaseVersion?> GetMaxVersion(Specification<Release> specification, CancellationToken token = default);
    IAsyncEnumerable<Release> GetCurrentReleases(Range range);
    IAsyncEnumerable<Release> GetObsoleteReleases(Range range);
}
