using ErrorOr;
using Mari.Application.Comments.Results;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Common.Specifications;
using MediatR;

namespace Mari.Application.Comments.Queries.GetSystemComments;

public class GetSystemCommentsQueryHandler : IRequestHandler<GetSystemCommentsQuery, ErrorOr<IList<CommentResult>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public GetSystemCommentsQueryHandler(
        ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IList<CommentResult>>> Handle(GetSystemCommentsQuery request, CancellationToken cancellationToken)
    {
        var isSystemSpec = CommentSpecs.IsSystem();
        var userIdIsSpec = CommentSpecs.ReleaseIdIs(request.ReleaseId);

        var comments = await _commentRepository.FindMany(isSystemSpec & userIdIsSpec, request.Range)
            .ToListAsync(cancellationToken);

        if (comments.Count == 0) return new List<CommentResult>();
        var users = await _userRepository.GetById(comments.Select(c => c.UserId), cancellationToken)
            .ToDictionaryAsync(u => u.Id, u => u.Username, cancellationToken);

        return comments.Select(c => CommentResult.FromComment(c, users[c.UserId])).ToList();
    }
}
