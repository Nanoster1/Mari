using Mari.Domain.Releases;
using Mari.Domain.Releases.ValueObjects;
using Mari.Infrastructure.Persistence.Configurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mari.Infrastructure.Persistence.Configurations;

public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
{
    public void Configure(EntityTypeBuilder<Release> builder)
    {
        builder.Property(r => r.Id)
            .IsValueObjectWrapper<Guid, ReleaseId>()
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Description)
            .IsStringWrapper()
            .IsRequired();

        builder.Property(r => r.MainIssue)
            .IsStringWrapper()
            .IsRequired();

        builder.Property(r => r.CompleteDate)
            .IsValueObjectWrapper<DateTimeOffset, ReleaseCompleteDate>()
            .IsRequired();

        builder.Property(r => r.UpdateDate)
            .IsValueObjectWrapper<DateTimeOffset, ReleaseUpdateDate>()
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired();

        var version = builder.OwnsOne(r => r.Version, version =>
        {
            version.Property(v => v.Major)
                .IsRequired();

            version.Property(v => v.Minor)
                .IsRequired();

            version.Property(v => v.Patch)
                .IsRequired();
        });

        builder.HasOne(r => r.Platform)
            .WithMany()
            .IsRequired();

        builder.Navigation(r => r.Platform)
            .AutoInclude();
    }
}
