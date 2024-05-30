using Microsoft.EntityFrameworkCore;
using TrashGrounds.Template.Database.Postgres;

namespace TrashGrounds.Template.Bootstrap;

public static class TemplateDatabaseBootstrap
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TemplateDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}