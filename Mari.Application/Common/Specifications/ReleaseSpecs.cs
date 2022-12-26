using LinqSpecs;
using Mari.Application.Common.Specifications.Releases;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.Enums;

namespace Mari.Application.Common.Specifications;

internal static class ReleaseSpecs
{
    public static Specification<Release> StatusIn(params ReleaseStatus[] statuses) => new StatusIn(statuses);
    public static Specification<Release> PlatformIn(params Platform[] platforms) => new PlatformIn(platforms);
}
