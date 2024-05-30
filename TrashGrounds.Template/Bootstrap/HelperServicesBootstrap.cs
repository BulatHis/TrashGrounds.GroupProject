using TrashGrounds.Template.Services;
using TrashGrounds.Template.Services.Interfaces;

namespace TrashGrounds.Template.Bootstrap;

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