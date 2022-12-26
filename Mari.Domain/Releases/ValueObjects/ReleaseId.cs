using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record ReleaseId : ValueObjectWrapper<Guid, ReleaseId>, IHasDefaultValue<ReleaseId>
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public ReleaseId() { }

    public static ReleaseId Default => Create(default);
}
