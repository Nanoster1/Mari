using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Domain.Comments.ValueObjects;

public record CommentId : ValueObjectWrapper<Guid, CommentId>, IHasDefaultValue<CommentId>
{
    [Obsolete(PublicConstructorObsoleteMessage, true)]
    public CommentId() { }

    public static CommentId Default { get; } = CommentId.Create(default);
}
