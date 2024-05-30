using Microsoft.EntityFrameworkCore;
using TrashGrounds.Post.Database.Postgres;

namespace TrashGrounds.Post.Bootstrap;

public static class PostDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}