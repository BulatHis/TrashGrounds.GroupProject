using TrashGrounds.Comment.Database.Postgres.Configurations;
using TrashGrounds.Comment.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Comment.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, CommentConfiguration>();
        
        return services;
    }
}