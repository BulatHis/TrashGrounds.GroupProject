using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;

namespace TrashGrounds.Rate.Bootstrap;

public static class RateDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RateDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}