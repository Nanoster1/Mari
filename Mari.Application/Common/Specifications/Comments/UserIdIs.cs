using System.Linq.Expressions;
using LinqSpecs;
using Mari.Domain.Comments;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Common.Specifications.Comments;

public class UserIdIs : Specification<Comment>
{
    public UserIdIs(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; }

    public override Expression<Func<Comment, bool>> ToExpression()
    {
        return c => c.UserId == UserId;
    }
}
