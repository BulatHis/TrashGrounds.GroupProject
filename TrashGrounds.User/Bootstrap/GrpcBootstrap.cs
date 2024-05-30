using FileClient;
using TrashGrounds.User.gRPC.Services;

namespace TrashGrounds.User.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<FileService.FileServiceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:File"] ?? throw new InvalidOperationException());
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<FileExistsService>();

        return services;
    }

    public static void MapGrpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<UserInfoGrpcService>();
        app.MapGrpcService<UsedFilesService>();
    }
}