using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;

namespace Mari.Application.Releases.Commands.DeleteRelease;

public class DeleteReleaseCommandHandler : IRequestHandler<DeleteReleaseCommand, ErrorOr<Deleted>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReleaseCommandHandler(
        IReleaseRepository releaseRepository,
        IUnitOfWork unitOfWork)
    {
        _releaseRepository = releaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await _releaseRepository.GetById(request.Id, cancellationToken);
        if (release is null) return Errors.Release.ReleaseNotFound;
        await _releaseRepository.Delete(release, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Deleted;
    }
}
