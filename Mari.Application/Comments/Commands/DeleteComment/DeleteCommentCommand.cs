using ErrorOr;
using Mari.Domain.Comments.ValueObjects;
using MediatR;

namespace Mari.Application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand : IRequest<ErrorOr<Deleted>>
{
    public DeleteCommentCommand(Guid id)
    {
        Id = CommentId.Create(id);
    }

    public CommentId Id { get; }
}
