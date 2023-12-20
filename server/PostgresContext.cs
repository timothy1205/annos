using Microsoft.EntityFrameworkCore;

namespace annos_server;

public class PostgresContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=annos_db;Username=postgres;Password=dev;Database=postgres");
    }

}