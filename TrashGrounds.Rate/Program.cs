using System.Net;
using TrashGrounds.Rate.Bootstrap;
using TrashGrounds.Rate.gRPC.Services;
using TrashGrounds.Rate.Infrastructure;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Middleware;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
builder.Host.AddCustomLogging();

builder.Services
    .AddDatabase(builder.Configuration)
    .AddGrpc();

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCustomEndpoints();
app.MapGrpcService<PostRateService>();
app.MapGrpcService<TrackRateService>();

app.Run();