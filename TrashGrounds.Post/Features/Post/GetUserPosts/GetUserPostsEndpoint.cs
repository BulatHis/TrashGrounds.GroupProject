using MediatR;
using TrashGrounds.Post.Infrastructure.Routing;

namespace TrashGrounds.Post.Features.Post.GetUserPosts;

public class GetUserPostsEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/from-user/{userId:guid}",
            async (Guid userId, int take, int? skip, IMediator mediator) =>
                Results.Ok(await mediator.Send(new GetUserPostsQuery(userId, take, skip ?? 0))));
    }
}