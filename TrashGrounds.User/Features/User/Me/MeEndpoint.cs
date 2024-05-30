using MediatR;
using TrashGrounds.User.Features.User.Profile;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.User.Me;

public class MeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/me",
            async (IUserService userService, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new GetProfileQuery(userService.GetUserIdOrThrow()))));
    }
}