using Grpc.Core;
using Grpc.Net.Client.Configuration;
using TrashGrounds.File.gRPC.Services;
using UsedFilesService = UsedFilesClient.UsedFilesService;

namespace TrashGrounds.File.Bootstrap;

public static class GrpcBootstrap
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultMethodConfig = new MethodConfig
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 5,
                InitialBackoff = TimeSpan.FromSeconds(1),
                MaxBackoff = TimeSpan.FromSeconds(5),
                BackoffMultiplier = 1.5,
                RetryableStatusCodes = { StatusCode.Unavailable }
            }
        };

        services.AddGrpc();

        services
            .AddGrpcClient<UsedFilesService.UsedFilesServiceClient>("UsedUser", options =>
            {
                options.Address = new Uri(configuration["UsedFiles:User"] ?? throw new InvalidOperationException());
            })
            .ConfigureChannel(options =>
            {
                options.ServiceConfig = new ServiceConfig {MethodConfigs = {defaultMethodConfig}};
            });

        services
            .AddGrpcClient<UsedFilesService.UsedFilesServiceClient>("UsedPost", options =>
            {
                options.Address = new Uri(configuration["UsedFiles:Post"] ?? throw new InvalidOperationException());
            })
            .ConfigureChannel(options =>
            {
                options.ServiceConfig = new ServiceConfig {MethodConfigs = {defaultMethodConfig}};
            });

        services
            .AddGrpcClient<UsedFilesService.UsedFilesServiceClient>("UsedTrack", options =>
            {
                options.Address = new Uri(configuration["UsedFiles:Track"] ?? throw new InvalidOperationException());
            })
            .ConfigureChannel(options =>
            {
                options.ServiceConfig = new ServiceConfig {MethodConfigs = {defaultMethodConfig}};
            });

        return services;
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<gRPC.Services.UsedFilesService>();
        
        return services;
    }
    
    public static void MapGrpcServices(this IEndpointRouteBuilder app)
    {
        app.MapGrpcService<FileExistsService>();
    }
}