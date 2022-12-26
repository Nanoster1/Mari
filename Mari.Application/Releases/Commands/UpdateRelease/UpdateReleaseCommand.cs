using ErrorOr;
using Mari.Application.Releases.Dto;
using Mari.Domain.Releases.Enums;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.UpdateRelease;

public record UpdateReleaseCommand : IRequest<ErrorOr<Updated>>
{
    public UpdateReleaseCommand(
        Guid id,
        ReleaseVersionDto version,
        string platformName,
        ReleaseStatus status,
        DateTimeOffset completeDate,
        string mainIssue,
        string description)
    {
        Id = ReleaseId.Create(id);
        Version = ReleaseVersion.Create(version.Major, version.Minor, version.Patch);
        PlatformName = PlatformName.Create(platformName);
        Status = status;
        CompleteDate = ReleaseCompleteDate.Create(completeDate);
        MainIssue = Issue.Create(mainIssue);
        Description = ReleaseDescription.Create(description);
    }

    public ReleaseId Id { get; }
    public ReleaseVersion Version { get; }
    public PlatformName PlatformName { get; }
    public ReleaseStatus Status { get; }
    public ReleaseCompleteDate CompleteDate { get; }
    public Issue MainIssue { get; }
    public ReleaseDescription Description { get; }
}
