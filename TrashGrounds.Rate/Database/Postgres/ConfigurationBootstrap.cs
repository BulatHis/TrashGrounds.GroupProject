using TrashGrounds.Rate.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Rate.Database.Postgres.Configurations.RateConfigurations;

namespace TrashGrounds.Rate.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, PostRateConfiguration>();
        services.AddSingleton<DependencyInjectedEntityConfiguration, TrackRateConfiguration>();
        
        return services;
    }
}