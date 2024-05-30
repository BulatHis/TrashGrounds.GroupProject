using TrashGrounds.Comment.Infrastructure.Mediator.Query;
using TrashGrounds.Comment.Models.Additional;

namespace TrashGrounds.Comment.Features.Comment.GetTrackComments;

public record GetTrackCommentsQuery(Guid TrackId, int Take, int Skip) : IQuery<IEnumerable<FullComment>>;