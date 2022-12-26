using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Comments.ValueObjects;

public record CommentContent : ValueObjectWrapper<string, CommentContent>, IStringWrapper
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public CommentContent() { }

    public static string Pattern => @".+";
    public static uint? MaxLength => 1000;
    public static uint? MinLength => 1;
}
