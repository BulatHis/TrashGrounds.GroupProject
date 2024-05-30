using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Database.Postgres;

public class TrackDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public required DbSet<MusicTrack> MusicTracks { get; set; }
    public required DbSet<Genre> Genres { get; set; }

    public TrackDbContext(DbContextOptions<TrackDbContext> options, 
        IEnumerable<DependencyInjectedEntityConfiguration> configurations) : base(options)
    {
        _configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in _configurations)
            configuration.Configure(builder);
        
        builder.Entity<Genre>().HasData(new List<Genre>
        {
            new()
            {
                Name = "Русский рэп",
            },
            new()
            {
                Name = "Что-то странное",
            },
            new()
            {
                Name = "Мэшап",
            }
        });
    }
    
    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}