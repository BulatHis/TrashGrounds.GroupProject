using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetBestRatedTracks;

public class GetBestRatedTracksQueryHandler : IQueryHandler<GetBestRatedTracksQuery, TracksRateResponse>
{
    private readonly RateDbContext _context;

    public GetBestRatedTracksQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<TracksRateResponse> Handle(GetBestRatedTracksQuery request, CancellationToken cancellationToken)
    {
        var bestTracks = await _context.TrackRates
            .GroupBy(rate => rate.TrackId)
            .OrderByDescending(g => g.Average(rate => rate.Rate))
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(g => new TrackRateResponse(g.Key, g.Average(r => r.Rate)))
            .ToListAsync(cancellationToken: cancellationToken);

        return new TracksRateResponse(bestTracks);
    }
}