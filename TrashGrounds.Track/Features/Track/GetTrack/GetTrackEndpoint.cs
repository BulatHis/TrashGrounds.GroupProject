using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{trackId:guid}", async (Guid trackId, IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetTrackQuery(trackId))));
    }
}