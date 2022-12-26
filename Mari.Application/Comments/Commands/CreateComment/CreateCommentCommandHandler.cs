using ErrorOr;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Common;
using Mari.Domain.Comments;
using MediatR;

namespace Mari.Application.Comments.Commands.CreateComment;

internal class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Created>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IReleaseRepository _releaseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IReleaseRepository releaseRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _releaseRepository = releaseRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        if (!await _releaseRepository.Exists(request.ReleaseId, cancellationToken))
            errors.Add(Errors.Release.ReleaseNotFound);

        if (!await _userRepository.Exists(request.UserId, cancellationToken))
            errors.Add(Errors.User.UserNotFound);

        var currentDate = _dateTimeProvider.UtcNow;

        var comment = Comment.Create(
            request.UserId,
            request.ReleaseId,
            request.Content,
            currentDate);

        if (comment.IsError) errors.AddRange(comment.Errors);

        if (errors.Count > 0) return errors;

        await _commentRepository.Insert(comment.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}
