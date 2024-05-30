using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public record GetTracksFromUserQuery(
        Guid UserId,
        Guid? ExcludeTrackId,
        int Take,
        int Skip) : IQuery<IEnumerable<FullTrackInfo>>;