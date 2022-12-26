using Mari.Domain.Users;
using Mari.Domain.Users.ValueObjects;
using Mari.Infrastructure.Persistence.Configurations.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mari.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
            .IsValueObjectWrapper<string, UserId>()
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Username)
            .IsStringWrapper()
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired();
    }
}
