using ErrorOr;
using Mari.Domain.Common.Models;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Domain.Releases.Entities;

public class Platform : Entity<PlatformId>
{
    public static ErrorOr<Platform> Create(PlatformName name)
    {
        return new Platform(
            name: name
        );
    }

    private Platform()
    {
    }

    private Platform(PlatformName name)
    {
        Name = name;
    }

    public PlatformName Name { get; private set; } = null!;

    public ErrorOr<Updated> ChangeName(PlatformName name)
    {
        Name = name;
        return Result.Updated;
    }
}
