using ErrorOr;
using Mari.Application.Releases.Dto;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.CreateRelease;

public record CreateReleaseCommand : IRequest<ErrorOr<Created>>
{
    public CreateReleaseCommand(
        string mainIssue,
        DateTimeOffset completeDate,
        string platformName,
        ReleaseVersionDto version,
        string description)
    {
        MainIssue = Issue.Create(mainIssue);
        CompleteDate = ReleaseCompleteDate.Create(completeDate);
        PlatformName = PlatformName.Create(platformName);
        Version = ReleaseVersion.Create(version.Major, version.Minor, version.Patch);
        Description = ReleaseDescription.Create(description);
    }

    public Issue MainIssue { get; }
    public ReleaseCompleteDate CompleteDate { get; }
    public PlatformName PlatformName { get; }
    public ReleaseVersion Version { get; }
    public ReleaseDescription Description { get; }
}
