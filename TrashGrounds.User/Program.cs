using System.Net;
using TrashGrounds.User.Bootstrap;
using TrashGrounds.User.Infrastructure;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();
ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;

builder.Services
    .AddDatabaseWithIdentity(builder.Configuration);
    //.AddRedis(builder.Configuration);

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAuthorizationWithPolicy();

builder.Services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

builder.Services
    .AddGrpcConfiguration(builder.Configuration)
    .AddGrpcServices();

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomEndpoints();
app.MapGrpcServices();

app.Run();