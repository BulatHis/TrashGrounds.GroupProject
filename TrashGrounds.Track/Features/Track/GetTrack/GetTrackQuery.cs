using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public record GetTrackQuery(Guid Id) : IQuery<FullTrack>;