using Microsoft.EntityFrameworkCore;
using TrashGrounds.Template.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Template.Database.Postgres;

public class TemplateDbContext : DbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public TemplateDbContext(DbContextOptions<TemplateDbContext> options, 
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
}