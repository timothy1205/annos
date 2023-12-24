using annos_server.Models;
using Microsoft.EntityFrameworkCore;

namespace annos_server;

public class PostgresContext : DbContext
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=annos_db;Username=postgres;Password=dev;Database=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscription>()
        .HasOne(s => s.SubscriptionCategory)
        .WithMany()
        .HasForeignKey(s => s.SubscriptionCategoryId);
    }

}