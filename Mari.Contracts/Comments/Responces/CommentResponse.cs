namespace Mari.Contracts.Comments.Responces;

public record CommentResponse(
    Guid CommentId,
    Guid ReleaseId,
    int UserId,
    string UserName,
    string Content,
    DateTimeOffset CreateDate,
    bool IsRedacted);

