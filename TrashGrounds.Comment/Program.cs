using System.Net;
using TrashGrounds.Comment.Bootstrap;
using TrashGrounds.Comment.Infrastructure;
using TrashGrounds.Comment.Infrastructure.Routing;
using TrashGrounds.Comment.Middleware;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
builder.Host.AddCustomLogging();

builder.Services
    .AddDatabase(builder.Configuration);
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

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseRouting();

app.UseAuthorization();

app.UseCustomEndpoints();

app.Run();