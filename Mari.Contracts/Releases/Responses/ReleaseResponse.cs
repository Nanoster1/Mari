using Mari.Contracts.Releases.Dto;

namespace Mari.Contracts.Releases.Responses;

public record ReleaseResponse(
    Guid Id,
    ReleaseVersionDto Version,
    string PlatformName,
    string Status,
    DateTimeOffset CompleteDate,
    DateTimeOffset UpdateDate,
    string MainIssue,
    string Description);
