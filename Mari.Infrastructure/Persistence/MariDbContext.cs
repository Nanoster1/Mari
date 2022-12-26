using Mari.Domain.Comments;
using Mari.Domain.Releases;
using Mari.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mari.Infrastructure.Persistence;

public class MariDbContext : DbContext
{
    public const string ConnectionString = "Database";

    public MariDbContext(DbContextOptions options) : base(options)
    {
    }

    DbSet<User> Users { get; set; } = null!;
    DbSet<Comment> Comments { get; set; } = null!;
    DbSet<Release> Releases { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MariDbContext).Assembly);
    }
}
