using Microsoft.EntityFrameworkCore;

namespace SystemDesign.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Register DbSet<TEntity> properties here as domain entities are added.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Applies every IEntityTypeConfiguration<T> defined in this assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
