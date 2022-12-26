using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Releases.Dto;

public record struct ReleaseVersionDto(int Major, int Minor, int Patch)
{
    public static ReleaseVersionDto FromVersion(ReleaseVersion version) => new(version.Major, version.Minor, version.Patch);
}
