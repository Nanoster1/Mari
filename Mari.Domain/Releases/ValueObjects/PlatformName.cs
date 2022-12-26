using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record PlatformName : ValueObjectWrapper<string, PlatformName>, IStringWrapper
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public PlatformName() { }

    public static string Pattern => @"^[^\d\W]+.*";
    public static uint? MaxLength => 100;
    public static uint? MinLength => 3;

}

