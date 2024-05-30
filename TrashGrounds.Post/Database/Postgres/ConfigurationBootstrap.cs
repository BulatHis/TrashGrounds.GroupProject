using TrashGrounds.Post.Database.Postgres.Configurations;
using TrashGrounds.Post.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Post.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, PostConfiguration>();
        
        return services;
    }
}