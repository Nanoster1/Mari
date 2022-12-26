using ErrorOr;
using Mari.Application.Releases.Dto;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.CreateDraftRelease;

public record CreateDraftReleaseCommand : IRequest<ErrorOr<Created>>
{
    public CreateDraftReleaseCommand(
        string? mainIssue,
        DateTimeOffset? completeDate,
        string? platformName,
        string? description,
        ReleaseVersionDto version)
    {
        if (mainIssue is not null)
            MainIssue = Issue.Create(mainIssue);

        if (completeDate is not null)
            CompleteDate = ReleaseCompleteDate.Create(completeDate.Value);

        if (platformName is not null)
            PlatformName = PlatformName.Create(platformName);

        Version = ReleaseVersion.Create(
            version.Major,
            version.Minor,
            version.Patch);

        if (description is not null)
            Description = ReleaseDescription.Create(description);
    }

    public Issue? MainIssue { get; }
    public ReleaseCompleteDate? CompleteDate { get; }
    public PlatformName? PlatformName { get; }
    public ReleaseVersion? Version { get; }
    public ReleaseDescription? Description { get; }
}
