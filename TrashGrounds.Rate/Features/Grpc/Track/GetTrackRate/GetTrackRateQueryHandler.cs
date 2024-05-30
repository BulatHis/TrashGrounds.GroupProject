using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;

namespace TrashGrounds.Rate.Features.Grpc.Track.GetTrackRate;

public class GetTrackRateQueryHandler : IQueryHandler<GetTrackRateQuery, double>
{
    private readonly RateDbContext _context;

    public GetTrackRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<double> Handle(GetTrackRateQuery request, CancellationToken cancellationToken)
    {
        return await _context.TrackRates.Where(rate => rate.TrackId == request.TrackId)
            .AverageAsync(rate => (double?)rate.Rate, cancellationToken: cancellationToken) ?? 0;
    }
}