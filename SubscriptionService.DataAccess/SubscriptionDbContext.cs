using Microsoft.EntityFrameworkCore;
using SubscriptionService.Models;

namespace SubscriptionService.DataAccess;

public class SubscriptionDbContext : DbContext
{
    private readonly string _dbPath;

    public DbSet<Subscription>? Subscriptions { get; set; }

    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        _dbPath = Path.Join(path, "locasubs.db");
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}
