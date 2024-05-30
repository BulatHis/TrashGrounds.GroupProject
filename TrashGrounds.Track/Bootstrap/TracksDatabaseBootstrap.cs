using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;

namespace TrashGrounds.Track.Bootstrap;

public static class TracksDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrackDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}