using TrashGrounds.Comment.Services;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Bootstrap;

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