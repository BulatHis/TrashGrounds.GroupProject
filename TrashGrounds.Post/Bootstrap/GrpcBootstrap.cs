using FileClient;
using TrashGrounds.Post.gRPC.Services;
using PostRateService = PostRateClient.PostRateService;

namespace TrashGrounds.Post.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddGrpcClient<PostRateService.PostRateServiceClient>(options =>
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
        services.AddScoped<gRPC.Services.PostRateService>();
        services.AddScoped<FileExistsService>();

        return services;
    }
    
    public static void MapGrpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<UsedFilesService>();
    }
}