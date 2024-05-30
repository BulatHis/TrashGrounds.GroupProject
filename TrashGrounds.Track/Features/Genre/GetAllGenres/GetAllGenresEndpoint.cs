using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Genre.GetAllGenres;

public class GetAllGenresEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/all", async (IMediator mediator) =>
            Results.Ok(await mediator.Send(new GetAllGenresQuery())));
    }
}