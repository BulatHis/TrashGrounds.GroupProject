using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public class DeletePostRateEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/",
                async ([FromRoute] Guid postId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeletePostRateCommand(
                        userService.GetUserIdOrThrow(),
                        postId))))
            .RequireAuthorization();
    }
}