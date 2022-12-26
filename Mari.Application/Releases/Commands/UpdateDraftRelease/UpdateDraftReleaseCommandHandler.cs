using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using Mari.Domain.Releases.Entities;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Domain.Releases.Enums;

namespace Mari.Application.Releases.Commands.UpdateDraftRelease;

public class UpdateDraftReleaseCommandHandler : IRequestHandler<UpdateDraftReleaseCommand, ErrorOr<Updated>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDraftReleaseCommandHandler(
        IReleaseRepository releaseRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _releaseRepository = releaseRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateDraftReleaseCommand request, CancellationToken cancellationToken)
    {
        if (!await _releaseRepository.Exists(request.Id, cancellationToken))
            return Errors.Release.ReleaseNotFound;

        var errors = new List<Error>();

        Platform? platform = null;
        if (request.PlatformName is not null)
        {
            var platformCreateResult = Platform.Create(request.PlatformName);
            if (platformCreateResult.IsError) errors.AddRange(platformCreateResult.Errors);
        }

        var release = await _releaseRepository.GetById(request.Id);
        if (release is null) return Errors.Release.ReleaseNotFound;
        if (release.Status is not ReleaseStatus.Draft) return Errors.Release.ReleaseMustBeDraft;

        var currentDateTime = _dateTimeProvider.UtcNow;

        if (request.Version is not null)
            errors.AddRange(release.ChangeVersion(request.Version, currentDateTime).Errors);

        if (request.CompleteDate is not null)
            errors.AddRange(release.ChangeCompleteDate(request.CompleteDate, currentDateTime).Errors);

        if (request.MainIssue is not null)
            errors.AddRange(release.ChangeMainIssue(request.MainIssue, currentDateTime).Errors);

        if (request.Description is not null)
            errors.AddRange(release.ChangeDescription(request.Description, currentDateTime).Errors);

        if (platform is not null) errors.AddRange(release.ChangePlatform(platform, currentDateTime).Errors);

        if (errors.Count > 0) return errors;
        await _releaseRepository.Update(release, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
