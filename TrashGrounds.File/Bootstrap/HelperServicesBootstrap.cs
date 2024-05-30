using TrashGrounds.File.Services;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Bootstrap;

public static class HelperServicesBootstrap
{
    public static IServiceCollection AddHelperServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IUserService, UserService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}