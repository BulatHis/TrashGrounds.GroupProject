using MediatR;
using TrashGrounds.Post.Features.Post.GetUserPosts;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.MyPosts;

public class GetMyPostsEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/my",
                async (int take, int? skip, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new GetUserPostsQuery(
                        userService.GetUserIdOrThrow(),
                        take,
                        skip ?? 0,
                        true))))
            .RequireAuthorization();
    }
}