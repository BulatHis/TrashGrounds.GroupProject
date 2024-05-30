using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Services.Configs;

namespace TrashGrounds.User.Bootstrap;

public static class UsersDatabaseBootstrap
{
    public static IServiceCollection AddDatabaseWithIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options=> {
                options.Password.RequiredLength = PasswordConfig.MinimumLength;
                options.Password.RequireNonAlphanumeric = PasswordConfig.RequireNonAlphanumeric;
                options.Password.RequireLowercase = PasswordConfig.RequireLowercase;
                options.Password.RequireUppercase = PasswordConfig.RequireUppercase;
                options.Password.RequireDigit = PasswordConfig.RequireDigit;
            })
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddDbContext<IUserDbContext,UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDatabaseConfigurations();

        return services;
    }
}