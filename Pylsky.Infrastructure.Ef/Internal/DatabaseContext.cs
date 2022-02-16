using Microsoft.EntityFrameworkCore;
using Pylsky.Infrastructure.Ef.Entities;

namespace Pylsky.Infrastructure.Ef.Internal;

internal class DatabaseContext : DbContext
{
    public DatabaseContext(DatabaseContextOptions options) : base(options.DbContextOptions)
    {
    }

    public DbSet<DeveloperEntity> Developers => Set<DeveloperEntity>();
    public DbSet<BugEntity> Bugs => Set<BugEntity>();
    public DbSet<FixEntity> Fixes => Set<FixEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FixEntity>()
            .HasOne<DeveloperEntity>()
            .WithMany()
            .HasForeignKey(x => x.DeveloperId);

        modelBuilder.Entity<FixEntity>()
            .HasOne<BugEntity>()
            .WithMany()
            .HasForeignKey(x => x.BugId);
    }
}