using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Comment.Infrastructure.Routing;
using TrashGrounds.Comment.Infrastructure.ValidationSetup;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.EditComment;

public class EditCommentEndpoint : IEndpoint
{
    public record EditCommentDto(string Message, Guid? ReplyTo);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{commentId:guid}",
                async (EditCommentDto dto, [FromRoute] Guid trackId, Guid commentId,
                        IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new EditCommentCommand(
                        userService.GetUserIdOrThrow(),
                        trackId,
                        commentId,
                        dto.Message,
                        dto.ReplyTo))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<EditCommentDto>());
    }
}