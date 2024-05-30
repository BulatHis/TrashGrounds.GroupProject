using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Rate.Models.Main;

namespace TrashGrounds.Rate.Database.Postgres;

public class RateDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public required DbSet<TrackRate> TrackRates { get; set; }
    
    public required DbSet<PostRate> PostRates { get; set; }

    public RateDbContext(DbContextOptions<RateDbContext> options, 
        IEnumerable<DependencyInjectedEntityConfiguration> configurations) : base(options)
    {
        _configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in _configurations)
            configuration.Configure(builder);
    }
    
    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}