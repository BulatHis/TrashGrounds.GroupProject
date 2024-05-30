using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Grpc.Post.GetPostsRate;

public class GetPostsRateQueryHandler : IQueryHandler<GetPostsRateQuery, PostsRateResponse>
{
    private readonly RateDbContext _context;

    public GetPostsRateQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<PostsRateResponse> Handle(GetPostsRateQuery request, CancellationToken cancellationToken)
    {
        var postsRate = await _context.PostRates
            .Where(rate => request.Ids.Contains(rate.PostId))
            .GroupBy(rate => rate.PostId)
            .Select(g => new {Id = g.Key, Rate = g.Sum(rate => rate.Rate)})
            .ToListAsync(cancellationToken: cancellationToken);

        var rates = request.Ids.Select(id => new PostRateResponse(
            id,
            postsRate.FirstOrDefault(r => r.Id == id)?.Rate ?? 0));
        
        return new PostsRateResponse(rates);
    }
}