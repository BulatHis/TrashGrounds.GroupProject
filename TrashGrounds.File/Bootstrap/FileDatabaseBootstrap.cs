using Microsoft.EntityFrameworkCore;
using TrashGrounds.File.Database.Postgres;

namespace TrashGrounds.File.Bootstrap;

public static class FileDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FileDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}