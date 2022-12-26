using Mari.Domain.Comments;
using Mari.Domain.Comments.ValueObjects;
using Mari.Domain.Releases;
using Mari.Domain.Users;
using Mari.Infrastructure.Persistence.Configurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mari.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Id)
            .IsValueObjectWrapper<Guid, CommentId>()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.IsRedacted)
            .IsRequired();

        builder.Property(c => c.IsSystem)
            .IsRequired();

        builder.Property(c => c.Content)
            .IsStringWrapper<CommentContent>()
            .IsRequired();

        builder.Property(c => c.CreateDate)
            .IsValueObjectWrapper<DateTimeOffset, CommentCreateDate>()
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);

        builder.HasOne<Release>()
            .WithMany()
            .HasForeignKey(c => c.ReleaseId);
    }
}
