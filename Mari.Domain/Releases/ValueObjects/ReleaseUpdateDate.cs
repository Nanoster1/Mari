using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record ReleaseUpdateDate : ValueObjectWrapper<DateTimeOffset, ReleaseUpdateDate>, IHasDefaultValue<ReleaseUpdateDate>
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public ReleaseUpdateDate() { }

    public static ReleaseUpdateDate Default => Create(default);
}
