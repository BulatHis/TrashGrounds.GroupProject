using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public record SearchTracksQuery(
    string? Query, 
    IEnumerable<Guid>? GenresId,
    int Take,
    int Skip) : IQuery<IEnumerable<FullTrackInfo>>;