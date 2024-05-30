using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetTracksRate;

public record GetTracksRateQuery(IEnumerable<Guid> Ids) : IQuery<TracksRateResponse>; 