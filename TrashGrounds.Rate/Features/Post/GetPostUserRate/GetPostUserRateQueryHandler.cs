using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Post.GetPostUserRate;

public class GetPostUserRateQueryHandler : IQueryHandler<GetPostUserRateQuery, PostUserRateResponse>
{
    private readonly RateDbContext _context;

    public GetPostUserRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<PostUserRateResponse> Handle(GetPostUserRateQuery request, CancellationToken cancellationToken)
    {
        var rate = await _context.PostRates.FirstOrDefaultAsync(
            rate => rate.PostId == request.PostId && rate.UserId == request.UserId,
            cancellationToken: cancellationToken);

        return new PostUserRateResponse(request.UserId, request.PostId, rate?.Rate ?? 0);
    }
}