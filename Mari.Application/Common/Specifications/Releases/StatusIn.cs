using System.Linq.Expressions;
using LinqSpecs;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Enums;

namespace Mari.Application.Common.Specifications.Releases;

internal class StatusIn : Specification<Release>
{
    public StatusIn(params ReleaseStatus[] statuses)
    {
        Statuses = statuses;
    }

    public ReleaseStatus[] Statuses { get; }

    public override Expression<Func<Release, bool>> ToExpression()
    {
        return r => Statuses.Contains(r.Status);
    }
}
