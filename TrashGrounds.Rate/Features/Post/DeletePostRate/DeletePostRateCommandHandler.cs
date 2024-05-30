using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure;
using TrashGrounds.Rate.Infrastructure.Mediator.Command;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public class DeletePostRateCommandHandler : ICommandHandler<DeletePostRateCommand, bool>
{
    private readonly RateDbContext _context;

    public DeletePostRateCommandHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePostRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _context.PostRates.FirstOrNotFoundAsync(
            rate => rate.PostId == request.PostId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        _context.PostRates.Remove(rate);
        await _context.SaveEntitiesAsync();
        return true;
    }
}