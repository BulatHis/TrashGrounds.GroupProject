using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.AddListen;

public class AddListenEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/{trackId:guid}/add-listen",
            async (Guid trackId, IMediator mediator) =>
                Results.Ok(await mediator.Send(new AddListenCommand(trackId))));
    }
}