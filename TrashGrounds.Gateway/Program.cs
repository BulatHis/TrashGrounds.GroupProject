using System.Net;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;

builder.Configuration.AddOcelot(
    builder.Environment.IsDevelopment() ? "./OcelotDev" : "./Ocelot", 
    builder.Environment);

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));
    

builder.Services
    .AddOcelot()
    .AddCacheManager(builderCachePart => builderCachePart.WithDictionaryHandle());

var app = builder.Build();

app.UseCors();
app.UseOcelot().Wait();

app.Run();