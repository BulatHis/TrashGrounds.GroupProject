using Microsoft.AspNetCore.Mvc;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.Auth.AuthCheck;

public class AuthCheckEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/current-id",
                ([FromServices] IUserService userService) => 
                    Results.Ok(userService.GetUserIdOrThrow()))
            .RequireAuthorization();
    }
}