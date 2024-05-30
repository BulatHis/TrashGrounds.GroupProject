using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetBestRatedTracks;

public record GetBestRatedTracksQuery(int Take, int Skip) : IQuery<TracksRateResponse>;