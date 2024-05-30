using TrashGrounds.Rate.Infrastructure.Mediator.Query;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetTrackRate;

public record GetTrackRateQuery(Guid TrackId) : IQuery<double>;