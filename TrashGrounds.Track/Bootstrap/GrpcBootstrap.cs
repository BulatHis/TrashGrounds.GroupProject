using FileClient;
using TrashGrounds.Track.gRPC.Services;
using UserClient;

namespace TrashGrounds.Track.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<UserMicroservice.UserMicroserviceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:User"] ?? throw new InvalidOperationException());
        });

        services.AddGrpcClient<TrackRateClient.TrackRateService.TrackRateServiceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:Rate"] ?? throw new InvalidOperationException());
        });
        
        services.AddGrpcClient<FileService.FileServiceClient>(options =>
        {
            options.Address = new Uri(configuration["Microservices:File"] ?? throw new InvalidOperationException());
        });
        
        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<UserInfoService>();
        services.AddScoped<TrackRateService>();
        services.AddScoped<FileExistsService>();

        return services;
    }
    
    public static void MapGrpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<UsedFilesService>();
    }
}