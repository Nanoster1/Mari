using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;

namespace Mari.Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<Deleted>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetById(request.Id);
        if (comment is null) return Errors.Comment.NotFound;
        await _commentRepository.Delete(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Deleted;
    }
}
