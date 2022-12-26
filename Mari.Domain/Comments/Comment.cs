using ErrorOr;
using Mari.Domain.Comments.ValueObjects;
using Mari.Domain.Common.Models;
using Mari.Domain.Releases.ValueObjects;
using Mari.Domain.Users.ValueObjects;
using Mari.Domain.Comments.Enums;
using Mari.Domain.Common;

namespace Mari.Domain.Comments;

public class Comment : AggregateRoot<CommentId>
{
    public static ErrorOr<Comment> Create(
        UserId userId,
        ReleaseId releaseId,
        CommentContent content,
        DateTimeOffset currentDate,
        bool isSystem = false)
    {
        return new Comment(
            userId: userId,
            releaseId: releaseId,
            content: content,
            createDate: CommentCreateDate.Create(currentDate),
            isSystem: isSystem);
    }

    private Comment()
    {
    }

    private Comment(
        UserId userId,
        ReleaseId releaseId,
        CommentContent content,
        CommentCreateDate createDate,
        bool isSystem)
    {
        UserId = userId;
        ReleaseId = releaseId;
        Content = content;
        CreateDate = createDate;
        IsSystem = isSystem;
        IsRedacted = false;
    }

    public static ErrorOr<Comment> CreateSystemComment(
        UserId userId,
        ReleaseId releaseId,
        SystemAction action,
        CommentCreateDate createDate,
        string? additionalInfo = null)
    {
        additionalInfo = string.IsNullOrWhiteSpace(additionalInfo)
            ? string.Empty
            : $"\n{additionalInfo}";

        var contentCreateResult = CommentContent.Create($"Действие: {action}{additionalInfo}");

        return Create(
            userId: userId,
            releaseId: releaseId,
            content: contentCreateResult,
            currentDate: createDate,
            isSystem: true);
    }

    public CommentContent Content { get; private set; } = null!;
    public UserId UserId { get; private set; } = null!;
    public ReleaseId ReleaseId { get; private set; } = null!;
    public CommentCreateDate CreateDate { get; private set; } = null!;
    public bool IsRedacted { get; private set; }
    public bool IsSystem { get; private set; }

    public ErrorOr<Updated> ChangeContent(CommentContent content)
    {
        if (IsSystem) return Errors.Comment.BlockedBySystem;
        IsRedacted = true;
        Content = content;
        return Result.Updated;
    }
}
