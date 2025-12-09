using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    // Constructor passes options (connection string, retry logic) to base
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define your DbSets (Tables)
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}