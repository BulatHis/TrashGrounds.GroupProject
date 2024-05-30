using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Command;
using TrashGrounds.Rate.Models.Additional.Track;
using TrashGrounds.Rate.Models.Main;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.ChangeTrackRate;

public class ChangeTrackRateCommandHandler : ICommandHandler<ChangeTrackRateCommand, TrackUserRateResponse>
{
    private readonly RateDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ChangeTrackRateCommandHandler(RateDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TrackUserRateResponse> Handle(ChangeTrackRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _context.TrackRates.FirstOrDefaultAsync(
            rate => rate.TrackId == request.TrackId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        if (rate is not null)
        {
            rate.Rate = request.NewRate;

            await _context.SaveEntitiesAsync();
            return new TrackUserRateResponse(rate.UserId, rate.TrackId, rate.Rate);
        }

        var newRate = new TrackRate
        {
            TrackId = request.TrackId,
            UserId = request.UserId,
            Rate = request.NewRate,
            DateTime = _dateTimeProvider.UtcNow
        };

        _context.TrackRates.Add(newRate);
        await _context.SaveEntitiesAsync();
        
        return new TrackUserRateResponse(newRate.UserId, newRate.TrackId, newRate.Rate);
    }
}