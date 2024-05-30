using MediatR;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.DeletePost;

public class DeletePostEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{postId:guid}",
                async (Guid postId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeletePostCommand(
                        userService.GetUserIdOrThrow(),
                        postId))))
            .RequireAuthorization();
    }
}