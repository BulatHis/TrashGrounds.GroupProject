using TrashGrounds.Track.Database.Postgres.Configurations;
using TrashGrounds.Track.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Track.Database.Postgres;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<DependencyInjectedEntityConfiguration, MusicTrackConfiguration>();
        
        return services;
    }
}