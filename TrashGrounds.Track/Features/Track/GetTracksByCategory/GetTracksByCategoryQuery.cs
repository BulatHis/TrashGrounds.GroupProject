using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public record GetTracksByCategoryQuery(
        Category Category,
        int Take,
        int Skip = 0) : IQuery<IEnumerable<FullTrackInfo>>;