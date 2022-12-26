using Mari.Application.Releases.Dto;
using Mari.Domain.Releases;

namespace Mari.Application.Releases.Results;

public record ReleaseResult(
    Guid Id,
    ReleaseVersionDto Version,
    string PlatformName,
    string Status,
    DateTimeOffset CompleteDate,
    DateTimeOffset UpdateDate,
    string MainIssue,
    string Description)
{
    public static ReleaseResult FromRelease(Release release) => new(
        Id: release.Id,
        Version: ReleaseVersionDto.FromVersion(release.Version),
        PlatformName: release.Platform.Name,
        Status: release.Status.ToString(),
        CompleteDate: release.CompleteDate,
        UpdateDate: release.UpdateDate,
        MainIssue: release.MainIssue,
        Description: release.Description);
}
