using ErrorOr;
using Mari.Domain.Comments.ValueObjects;
using MediatR;

namespace Mari.Application.Comments.Commands.UpdateComment;

public record UpdateCommentCommand : IRequest<ErrorOr<Updated>>
{
    public UpdateCommentCommand(Guid commentId, string content)
    {
        CommentId = CommentId.Create(commentId);
        Content = CommentContent.Create(content);
    }

    public CommentId CommentId { get; }
    public CommentContent Content { get; }
}
