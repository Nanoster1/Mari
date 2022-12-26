using ErrorOr;
using Mari.Domain.Comments.ValueObjects;
using Mari.Domain.Releases.ValueObjects;
using Mari.Domain.Users.ValueObjects;
using MediatR;

namespace Mari.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand : IRequest<ErrorOr<Created>>
{
    public CreateCommentCommand(
        string userId,
        Guid releaseId,
        string content)
    {
        UserId = UserId.Create(userId);
        ReleaseId = ReleaseId.Create(releaseId);
        Content = CommentContent.Create(content); ;
    }

    public UserId UserId { get; }
    public ReleaseId ReleaseId { get; }
    public CommentContent Content { get; }
}
