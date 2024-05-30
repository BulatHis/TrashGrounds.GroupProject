using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.GetPostUserRate;

public class GetPostUserRateEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/",
                async ([FromRoute] Guid postId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new GetPostUserRateQuery(
                        userService.GetUserIdOrThrow(),
                        postId))))
            .RequireAuthorization();
    }
}