using ErrorOr;
using Mari.Application.Releases.Dto;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.UpdateDraftRelease;

public record UpdateDraftReleaseCommand : IRequest<ErrorOr<Updated>>
{
    public UpdateDraftReleaseCommand(
        Guid id,
        ReleaseVersionDto? version,
        string? platformName,
        DateTimeOffset? completeDate,
        string? mainIssue,
        string? description)
    {
        Id = ReleaseId.Create(id);

        if (version is not null)
            Version = ReleaseVersion.Create(version.Value.Major, version.Value.Minor, version.Value.Patch);

        if (platformName is not null)
            PlatformName = PlatformName.Create(platformName);

        if (completeDate is not null)
            CompleteDate = ReleaseCompleteDate.Create(completeDate.Value);

        if (mainIssue is not null)
            MainIssue = Issue.Create(mainIssue);

        if (description is not null)
            Description = ReleaseDescription.Create(description);
    }

    public ReleaseId Id { get; }
    public ReleaseVersion? Version { get; }
    public PlatformName? PlatformName { get; }
    public ReleaseCompleteDate? CompleteDate { get; }
    public Issue? MainIssue { get; }
    public ReleaseDescription? Description { get; }
}
