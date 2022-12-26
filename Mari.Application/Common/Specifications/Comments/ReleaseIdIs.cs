using System.Linq.Expressions;
using LinqSpecs;
using Mari.Domain.Comments;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Common.Specifications.Comments;

internal class ReleaseIdIs : Specification<Comment>
{
    public ReleaseIdIs(ReleaseId releaseId)
    {
        ReleaseId = releaseId;
    }

    public ReleaseId ReleaseId { get; }

    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return c => c.ReleaseId == ReleaseId;
    }
}
