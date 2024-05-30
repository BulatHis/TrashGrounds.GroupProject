using MediatR;
using TrashGrounds.File.Infrastructure.Routing;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Features.Image.UploadImage;

public class UploadImageEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/upload",
                async (IFormFile file, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new UploadImageCommand(
                        file,
                        userService.GetUserIdOrThrow()))))
            .RequireAuthorization();
    }
}