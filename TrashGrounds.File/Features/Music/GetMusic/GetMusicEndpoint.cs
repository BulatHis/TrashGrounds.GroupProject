using MediatR;
using TrashGrounds.File.Infrastructure.Routing;

namespace TrashGrounds.File.Features.Music.GetMusic;

public class GetMusicEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}",
            async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetMusicQuery(id));
                return Results.File(
                    response.Stream, 
                    response.ContentType, 
                    response.FileId.ToString(), 
                    null, 
                    null, 
                    true);
            });
    }
}