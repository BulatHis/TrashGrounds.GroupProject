using TrashGrounds.Comment.gRPC.Services;
using UserClient;

namespace TrashGrounds.Comment.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<UserMicroservice.UserMicroserviceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:User"] ?? throw new InvalidOperationException());
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<UserInfoService>();

        return services;
    }
}