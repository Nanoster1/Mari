using ErrorOr;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.Enums;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.CreateRelease;

internal class CreateReleaseCommandHandler : IRequestHandler<CreateReleaseCommand, ErrorOr<Created>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateReleaseCommandHandler(
        IReleaseRepository releaseRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _releaseRepository = releaseRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Created>> Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
    {
        var platform = await _releaseRepository.GetPlatformByName(request.PlatformName, cancellationToken);

        if (platform is null)
        {
            var platformCreateResult = Platform.Create(request.PlatformName);
            if (platformCreateResult.IsError) return platformCreateResult.Errors;
            platform = platformCreateResult.Value;
        }

        var releaseCreateResult = Release.Create(
            mainIssue: request.MainIssue,
            platform: platform,
            completeDate: request.CompleteDate,
            currentDate: _dateTimeProvider.UtcNow,
            version: request.Version,
            description: request.Description,
            status: ReleaseStatus.Planning);

        if (releaseCreateResult.IsError) return releaseCreateResult.Errors;

        await _releaseRepository.Insert(releaseCreateResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}
