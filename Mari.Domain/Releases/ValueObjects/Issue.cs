using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Releases.ValueObjects;

public record Issue : ValueObjectWrapper<string, Issue>, IStringWrapper
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public Issue() { }

    public static string Pattern => @"(http|ftp|https):\/\/([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:\/~+#-]*[\w@?^=%&\/~+#-])";
    public static uint? MaxLength => null;
    public static uint? MinLength => 10;
}
