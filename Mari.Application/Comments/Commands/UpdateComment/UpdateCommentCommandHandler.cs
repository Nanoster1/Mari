using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;

namespace Mari.Application.Comments.Commands.UpdateComment;

internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ErrorOr<Updated>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetById(request.CommentId, cancellationToken);
        if (comment is null) return Errors.Comment.NotFound;
        var result = comment.ChangeContent(request.Content);
        if (result.IsError) return result.Errors;
        await _commentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
