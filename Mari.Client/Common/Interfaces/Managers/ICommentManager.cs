using Mari.Client.Models.Comments;

namespace Mari.Client.Common.Interfaces.Managers;

public interface ICommentManager
{
    Task Create(CommentModel comment, CancellationToken token = default);
    Task<IList<CommentModel>> GetAllUserComment(Guid releaseId, CancellationToken token = default);
    Task<IList<CommentModel>> GetAllSystemComment(Guid releaseId, CancellationToken token = default);
    Task UpdateComment(CommentModel comment, CancellationToken token = default);
    Task DeleteComment(Guid commmentId, CancellationToken token = default);
}
