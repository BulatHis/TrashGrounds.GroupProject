using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Command;
using TrashGrounds.Rate.Models.Additional.Post;
using TrashGrounds.Rate.Models.Main;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.ChangePostRate;

public class ChangePostRateCommandHandler : ICommandHandler<ChangePostRateCommand, PostUserRateResponse>
{
    private readonly RateDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ChangePostRateCommandHandler(RateDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<PostUserRateResponse> Handle(ChangePostRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _context.PostRates.FirstOrDefaultAsync(
            rate => rate.PostId == request.PostId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        if (rate is not null)
        {
            rate.Rate = request.NewRate;

            await _context.SaveEntitiesAsync();
            return new PostUserRateResponse(rate.UserId, rate.PostId, rate.Rate);
        }

        var newRate = new PostRate
        {
            PostId = request.PostId,
            UserId = request.UserId,
            Rate = request.NewRate,
            DateTime = _dateTimeProvider.UtcNow
        };

        _context.PostRates.Add(newRate);
        await _context.SaveEntitiesAsync();
        
        return new PostUserRateResponse(newRate.UserId, newRate.PostId, newRate.Rate);
    }
}