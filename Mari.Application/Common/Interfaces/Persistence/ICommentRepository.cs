using Mari.Application.Common.Interfaces.Persistence.Shared;
using Mari.Domain.Comments;
using Mari.Domain.Comments.ValueObjects;

namespace Mari.Application.Common.Interfaces.Persistence;

public interface ICommentRepository : IRepository<Comment, CommentId>
{
}
