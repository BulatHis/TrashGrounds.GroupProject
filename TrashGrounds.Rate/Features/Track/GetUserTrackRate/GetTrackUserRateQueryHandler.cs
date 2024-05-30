using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Track;

namespace TrashGrounds.Rate.Features.Track.GetUserTrackRate;

public class GetTrackUserRateQueryHandler : IQueryHandler<GetTrackUserRateQuery, TrackUserRateResponse>
{
    private readonly RateDbContext _context;

    public GetTrackUserRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<TrackUserRateResponse> Handle(GetTrackUserRateQuery request, CancellationToken cancellationToken)
    {
        var rate = await _context.TrackRates.FirstOrDefaultAsync(
            rate => rate.TrackId == request.TrackId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        return new TrackUserRateResponse(request.UserId, request.TrackId, rate?.Rate ?? 0);
    }
}