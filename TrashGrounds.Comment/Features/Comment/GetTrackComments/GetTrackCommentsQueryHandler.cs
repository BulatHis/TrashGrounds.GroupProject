using Microsoft.EntityFrameworkCore;
using TrashGrounds.Comment.Database.Postgres;
using TrashGrounds.Comment.gRPC.Services;
using TrashGrounds.Comment.Infrastructure.Mediator.Query;
using TrashGrounds.Comment.Models.Additional;

namespace TrashGrounds.Comment.Features.Comment.GetTrackComments;

public class GetTrackCommentsQueryHandler : IQueryHandler<GetTrackCommentsQuery, IEnumerable<FullComment>>
{
    private readonly CommentDbContext _context;
    private readonly UserInfoService _userInfo;

    public GetTrackCommentsQueryHandler(CommentDbContext context, UserInfoService userInfo)
    {
        _context = context;
        _userInfo = userInfo;
    }

    public async Task<IEnumerable<FullComment>> Handle(GetTrackCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _context.Comments
            .Where(comment => comment.TrackId == request.TrackId)
            .OrderByDescending(comment => comment.SendAt)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken: cancellationToken);

        var users = await _userInfo.GetUsersInfoAsync(
            comments.Select(comment => comment.UserId));

        return comments.Select(comment => new FullComment
        {
            Comment = comment,
            UserInfo = users?.FirstOrDefault(user => user.Id == comment.UserId)
        });
    }
}