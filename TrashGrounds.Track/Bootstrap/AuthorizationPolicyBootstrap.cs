namespace TrashGrounds.Track.Bootstrap;

public static class AuthorizationPolicyBootstrap
{
    public static IServiceCollection AddAuthorizationWithPolicy(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Moderator", policy => policy.RequireRole("Moderator", "Admin"));
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });
}