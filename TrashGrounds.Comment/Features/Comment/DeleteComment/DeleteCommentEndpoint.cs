using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Comment.Infrastructure.Routing;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.DeleteComment;

public class DeleteCommentEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{commentId:guid}",
                async ([FromRoute] Guid trackId, Guid commentId,
                        IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeleteCommentCommand(
                        userService.GetUserIdOrThrow(),
                        trackId,
                        commentId))))
            .RequireAuthorization();
    }
}