using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record ReleaseCompleteDate : ValueObjectWrapper<DateTimeOffset, ReleaseCompleteDate>
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public ReleaseCompleteDate() { }
}
