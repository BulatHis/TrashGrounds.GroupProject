using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;

namespace TrashGrounds.Rate.Features.Grpc.Post.GetPostRate;

public class GetPostRateQueryHandler : IQueryHandler<GetPostRateQuery, int>
{
    private readonly RateDbContext _context;

    public GetPostRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetPostRateQuery request, CancellationToken cancellationToken)
    {
        return await _context.PostRates
            .Where(rate => rate.PostId == request.PostId)
            .SumAsync(rate => rate.Rate, cancellationToken: cancellationToken);
    }
}