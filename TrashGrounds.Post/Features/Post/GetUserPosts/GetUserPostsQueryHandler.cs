using Microsoft.EntityFrameworkCore;
using TrashGrounds.Post.Database.Postgres;
using TrashGrounds.Post.gRPC.Services;
using TrashGrounds.Post.Infrastructure.Mediator.Query;
using TrashGrounds.Post.Models.Additional;

namespace TrashGrounds.Post.Features.Post.GetUserPosts;

public class GetUserPostsQueryHandler : IQueryHandler<GetUserPostsQuery, IEnumerable<PostWithRate>>
{
    private readonly PostDbContext _context;
    private readonly PostRateService _postRateService;

    public GetUserPostsQueryHandler(PostDbContext context, PostRateService postRateService)
    {
        _context = context;
        _postRateService = postRateService;
    }

    public async Task<IEnumerable<PostWithRate>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .Where(post => post.UserId == request.UserId && (request.ShowHidden || !post.IsHidden))
            .OrderByDescending(post => post.UploadDate)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        var rates = await _postRateService.GetPostsRateAsync(posts.Select(post => post.Id));
        
        return posts.Select(post => new PostWithRate(
            post,
            rates?.FirstOrDefault(rate => rate.PostId == post.Id)?.Rating));
    }
};