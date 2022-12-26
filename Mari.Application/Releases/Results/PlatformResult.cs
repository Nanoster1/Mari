using Mari.Application.Releases.Dto;

namespace Mari.Application.Releases.Results;

public record PlatformResult(string Name, ReleaseVersionDto LastVersion);
