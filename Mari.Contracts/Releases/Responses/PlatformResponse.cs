using Mari.Contracts.Releases.Dto;

namespace Mari.Contracts.Releases.Responses;

public record PlatformResponse(
    string Name,
    ReleaseVersionDto LastVersion);
