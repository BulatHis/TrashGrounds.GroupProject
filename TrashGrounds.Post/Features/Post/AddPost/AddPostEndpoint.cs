using MediatR;
using TrashGrounds.Post.Infrastructure.Routing;
using TrashGrounds.Post.Infrastructure.ValidationSetup;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.AddPost;

public class AddPostEndpoint : IEndpoint
{
    public record AddPostDto(string Text, Guid? AssetId, bool IsHidden = false);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/add", async (AddPostDto dto, IUserService userService, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddPostCommand(
                    userService.GetUserIdOrThrow(),
                    dto.Text,
                    dto.AssetId,
                    dto.IsHidden))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<AddPostDto>());
    }
}