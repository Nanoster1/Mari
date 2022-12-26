using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record PlatformId : ValueObjectWrapper<int, PlatformId>, IHasDefaultValue<PlatformId>
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public PlatformId() { }

    public static PlatformId Default => Create(default);
}
