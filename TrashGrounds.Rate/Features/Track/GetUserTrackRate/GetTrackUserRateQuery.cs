using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Track.GetUserTrackRate;

public record GetTrackUserRateQuery(Guid UserId, Guid TrackId) : IQuery<TrackUserRateResponse>;