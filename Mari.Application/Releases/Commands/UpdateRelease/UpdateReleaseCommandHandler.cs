using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using Mari.Domain.Releases.Entities;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Domain.Releases.Enums;

namespace Mari.Application.Releases.Commands.UpdateRelease;

public class UpdateReleaseCommandHandler : IRequestHandler<UpdateReleaseCommand, ErrorOr<Updated>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReleaseCommandHandler(
        IReleaseRepository releaseRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _releaseRepository = releaseRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
    {
        if (!await _releaseRepository.Exists(request.Id, cancellationToken))
            return Errors.Release.ReleaseNotFound;

        var errors = new List<Error>();

        var release = await _releaseRepository.GetById(request.Id);
        if (release is null) return Errors.Release.ReleaseNotFound;

        Platform? platform = null;
        if (request.PlatformName != release.Platform.Name)
        {
            platform = await _releaseRepository.GetPlatformByName(request.PlatformName);
            if (platform is null)
            {
                var platformCreateResult = Platform.Create(request.PlatformName);
                if (platformCreateResult.IsError) errors.AddRange(platformCreateResult.Errors);
                platform = platformCreateResult.Value;
            }
        }

        var currentDateTime = _dateTimeProvider.UtcNow;
        //TODO Complete и Developing из Plannig - не его зона ответственности
        if (request.Version != release.Version)
        {
            var result = release.ChangeVersion(request.Version, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (request.Status != release.Status)
        {
            var result = release.ChangeStatus((ReleaseStatus)request.Status, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (request.CompleteDate != release.CompleteDate)
        {
            var result = release.ChangeCompleteDate(request.CompleteDate, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (request.MainIssue != release.MainIssue)
        {
            var result = release.ChangeMainIssue(request.MainIssue, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (request.Description != release.Description)
        {
            var result = release.ChangeDescription(request.Description, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (platform is not null && platform != release.Platform)
        {
            var result = release.ChangePlatform(platform, currentDateTime);
            if (result.IsError) errors.AddRange(result.Errors);
        }

        if (errors.Count > 0) return errors;
        await _releaseRepository.Update(release, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Updated;
    }
}
