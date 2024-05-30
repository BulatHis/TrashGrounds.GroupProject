using Microsoft.EntityFrameworkCore;
using TrashGrounds.Comment.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Comment.Database.Postgres;

public class CommentDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;

    public required DbSet<Models.Main.Comment> Comments { get; set; }

    public CommentDbContext(DbContextOptions<CommentDbContext> options, 
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