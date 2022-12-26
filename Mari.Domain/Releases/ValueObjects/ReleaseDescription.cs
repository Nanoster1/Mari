using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record ReleaseDescription :
    ValueObjectWrapper<string, ReleaseDescription>,
    IStringWrapper,
    IHasDefaultValue<ReleaseDescription>
{
    [Obsolete(PublicConstructorObsoleteMessage)]
    public ReleaseDescription() { }

    public static ReleaseDescription Default => ReleaseDescription.Create(string.Empty);

    public static string Pattern => ".*";
    public static uint? MaxLength => 1000;
    public static uint? MinLength => null;
}
