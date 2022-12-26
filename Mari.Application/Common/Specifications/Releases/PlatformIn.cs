using System.Linq.Expressions;
using LinqSpecs;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;

namespace Mari.Application.Common.Specifications.Releases;

internal class PlatformIn : Specification<Release>
{
    public PlatformIn(params Platform[] platforms)
    {
        Platforms = platforms;
    }

    public Platform[] Platforms { get; }

    public override Expression<Func<Release, bool>> ToExpression()
    {
        return r => Platforms.Contains(r.Platform);
    }
}
