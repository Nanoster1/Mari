using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.ValueObjects;
using Mari.Infrastructure.Persistence.Configurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mari.Infrastructure.Persistence.Configurations;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.Property(p => p.Id)
            .IsValueObjectWrapper<int, PlatformId>()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsStringWrapper()
            .IsRequired();

        builder.HasIndex(p => p.Name).IsUnique();
    }
}
