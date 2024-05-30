using Microsoft.EntityFrameworkCore;
using TrashGrounds.Rate.Database.Postgres;
using TrashGrounds.Rate.Infrastructure.Mediator.Query;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Post.GetPostsUserRates;

public class GetPostsUserRatesQueryHandler : IQueryHandler<GetPostsUserRatesQuery, PostsUserRateResponse>
{
    private readonly RateDbContext _context;

    public GetPostsUserRatesQueryHandler(RateDbContext context)
    {
        _context = context;
    }

    public async Task<PostsUserRateResponse> Handle(GetPostsUserRatesQuery request, CancellationToken cancellationToken)
    {
        var postsRates = await _context.PostRates
            .Where(rate => rate.UserId == request.UserId && request.PostsId.Contains(rate.PostId))
            .Select(rate => new {rate.PostId, rate.Rate})
            .ToListAsync(cancellationToken: cancellationToken);

        var userRates = request.PostsId.Select(postId => new PostUserRateResponse(
                request.UserId,
                postId,
                postsRates.FirstOrDefault(post => post.PostId == postId)?.Rate ?? 0))
            .ToList();
        return new PostsUserRateResponse(userRates);
    }
}