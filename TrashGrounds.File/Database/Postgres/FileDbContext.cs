using Microsoft.EntityFrameworkCore;
using TrashGrounds.File.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.File.Models.Main;

namespace TrashGrounds.File.Database.Postgres;

public class FileDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public required DbSet<MusicFile> MusicFiles { get; set; }
    public required DbSet<ImageFile> ImageFiles { get; set; }

    public FileDbContext(DbContextOptions<FileDbContext> options, 
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