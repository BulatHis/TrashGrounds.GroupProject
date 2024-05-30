using TrashGrounds.Track.Features.Genre.GetAllGenres;
using TrashGrounds.Track.Infrastructure.Routing;

namespace TrashGrounds.Track.Features.Genre;

public class GenreEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/genre")
            .WithTags("Жанры")
            .AddEndpoint<GetAllGenresEndpoint>();
    }
}