using TrashGrounds.User.Database.Postgres.Configurations;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.User.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, AppUserConfiguration>()
            .AddSingleton<DependencyInjectedEntityConfiguration, DomainUserConfiguration>();
        
        return services;
    }
}