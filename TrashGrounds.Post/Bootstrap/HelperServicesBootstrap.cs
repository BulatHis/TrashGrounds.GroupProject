using TrashGrounds.Post.Services;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Bootstrap;

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