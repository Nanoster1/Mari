using ErrorOr;
using Mari.Application.Comments.Results;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Common.Specifications;
using MediatR;

namespace Mari.Application.Comments.Queries.GetComments;

internal class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, ErrorOr<IList<CommentResult>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public GetCommentsQueryHandler(
        ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IList<CommentResult>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var userIdIsSpec = CommentSpecs.ReleaseIdIs(request.ReleaseId);

        var comments = await _commentRepository.FindMany(userIdIsSpec, request.Range)
            .ToListAsync(cancellationToken);

        if (comments.Count == 0) return new List<CommentResult>();
        var users = await _userRepository.GetById(comments.Select(c => c.UserId), cancellationToken)
            .ToDictionaryAsync(u => u.Id, u => u.Username, cancellationToken);

        return comments.Select(c => CommentResult.FromComment(c, users[c.UserId])).ToList();
    }
}
