using LinqSpecs;
using Mari.Application.Common.Specifications.Comments;
using Mari.Domain.Comments;
using Mari.Domain.Releases.ValueObjects;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Common.Specifications;

internal static class CommentSpecs
{
    public static Specification<Comment> IsSystem() => new IsSystem();
    public static Specification<Comment> UserIdIs(UserId userId) => new UserIdIs(userId);
    public static Specification<Comment> ReleaseIdIs(ReleaseId releaseId) => new ReleaseIdIs(releaseId);
}
