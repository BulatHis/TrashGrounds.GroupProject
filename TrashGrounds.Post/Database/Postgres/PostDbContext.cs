using Microsoft.EntityFrameworkCore;
using TrashGrounds.Post.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Post.Database.Postgres;

public class PostDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public required DbSet<Models.Main.Post> Posts { get; set; }

    public PostDbContext(DbContextOptions<PostDbContext> options, 
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