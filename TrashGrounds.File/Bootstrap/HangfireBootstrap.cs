using Hangfire;
using Hangfire.PostgreSql;
using TrashGrounds.File.Hangfire;

namespace TrashGrounds.File.Bootstrap;

public static class HangfireBootstrap
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire")));

        services.AddHangfireServer(opt =>
        {
            opt.Queues = new[] {"gc", "default"};
            opt.WorkerCount = 1;
        });

        services.AddScoped<GarbageCollectorService>();
        
        return services;
    }

    public static void AddHangfireJobs()
    {
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-music", service =>
            service.DeleteUnusedTracks(), Cron.Daily);
        RecurringJob.AddOrUpdate<GarbageCollectorService>("clear-images", service =>
            service.DeleteUnusedImages(), Cron.Daily);
    }
}