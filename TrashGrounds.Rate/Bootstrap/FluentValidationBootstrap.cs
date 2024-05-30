using FluentValidation;
using FluentValidation.AspNetCore;

namespace TrashGrounds.Rate.Bootstrap;

public static class FluentValidationBootstrap
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
}