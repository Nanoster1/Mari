using Mari.Domain.Comments;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Comments.Results;

public record CommentResult(
    Guid CommentId,
    Guid ReleaseId,
    string UserId,
    string UserName,
    string Content,
    DateTimeOffset CreateDate,
    bool IsRedacted)
{
    public static CommentResult FromComment(Comment comment, Username username) => new(
        comment.Id,
        comment.ReleaseId,
        comment.UserId,
        username,
        comment.Content,
        comment.CreateDate,
        comment.IsRedacted);
}
