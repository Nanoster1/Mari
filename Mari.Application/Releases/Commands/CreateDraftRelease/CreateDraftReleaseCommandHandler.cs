using ErrorOr;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.Enums;
using MediatR;

namespace Mari.Application.Releases.Commands.CreateDraftRelease;

internal class CreateDraftReleaseCommandHandler : IRequestHandler<CreateDraftReleaseCommand, ErrorOr<Created>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateDraftReleaseCommandHandler(
        IReleaseRepository releaseRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _releaseRepository = releaseRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Created>> Handle(CreateDraftReleaseCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();
        Platform? platform = null;

        if (request.PlatformName is not null)
        {
            var platformCreateResult = Platform.Create(request.PlatformName);
            if (platformCreateResult.IsError) errors.AddRange(platformCreateResult.Errors);
            else platform = platformCreateResult.Value;
        }

        var releaseCreateError = Release.Create(
            mainIssue: request.MainIssue,
            platform: platform,
            completeDate: request.CompleteDate,
            currentDate: _dateTimeProvider.UtcNow,
            version: request.Version,
            description: request.Description,
            status: ReleaseStatus.Draft);

        if (releaseCreateError.IsError) errors.AddRange(releaseCreateError.Errors);
        if (errors.Count > 0) return errors;

        await _releaseRepository.Insert(releaseCreateError.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}
