using System.Linq.Expressions;
using LinqSpecs;
using Mari.Domain.Comments;

namespace Mari.Application.Common.Specifications.Comments;

internal class IsSystem : Specification<Comment>
{
    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return c => c.IsSystem;
    }
}
