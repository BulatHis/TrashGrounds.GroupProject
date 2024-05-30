using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetTracksRate;

public class GetTracksRateQueryHandler : IQueryHandler<GetTracksRateQuery, TracksRateResponse>
{
    private readonly RateDbContext _context;

    public GetTracksRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<TracksRateResponse> Handle(GetTracksRateQuery request, CancellationToken cancellationToken)
    {
        var tracksRate = await _context.TrackRates
            .Where(rate => request.Ids.Contains(rate.TrackId))
            .GroupBy(rate => rate.TrackId)
            .Select(g => new {Id = g.Key, Rate = g.Average(rate => rate.Rate)})
            .ToListAsync(cancellationToken: cancellationToken);

        var rates = request.Ids.Select(id => new TrackRateResponse(
            id,
            tracksRate.FirstOrDefault(r => r.Id == id)?.Rate ?? 0));
        
        return new TracksRateResponse(rates);
    }
}