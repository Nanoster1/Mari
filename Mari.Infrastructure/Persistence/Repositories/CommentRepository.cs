using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Comments;
using Mari.Domain.Comments.ValueObjects;
using Mari.Infrastructure.Persistence.Shared;

namespace Mari.Infrastructure.Persistence.Repositories;

public class CommentRepository : Repository<Comment, CommentId>, ICommentRepository
{
    public CommentRepository(MariDbContext context) : base(context)
    {
    }
}
