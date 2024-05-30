using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public class GetTracksByCategoryEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{category}", 
            async (Category category, int take, int? skip, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new GetTracksByCategoryQuery(category, take, skip ?? default))));
    }
}