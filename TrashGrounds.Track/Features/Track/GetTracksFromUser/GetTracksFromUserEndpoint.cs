using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/from-user/{id:guid}",
            async (Guid id, int take, Guid? excludeTrackId, int? skip, IMediator mediator) =>
                Results.Ok(await mediator.Send(new GetTracksFromUserQuery(
                    id,
                    excludeTrackId,
                    take,
                    skip ?? default))));
    }
}