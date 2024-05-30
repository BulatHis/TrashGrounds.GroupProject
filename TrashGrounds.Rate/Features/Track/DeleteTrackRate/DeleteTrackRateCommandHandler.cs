using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Command;

namespace TrashGrounds.Rate.Features.Track.DeleteTrackRate;

public class DeleteTrackRateCommandHandler : ICommandHandler<DeleteTrackRateCommand, bool>
{
    private readonly RateDbContext _context;

    public DeleteTrackRateCommandHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTrackRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _context.TrackRates.FirstOrDefaultAsync(
            rate => rate.TrackId == request.TrackId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        if (rate is null) 
            return true;

        _context.TrackRates.Remove(rate);
        await _context.SaveEntitiesAsync();
        return true;
    }
}