using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTracksFromUser;

public class GetTracksFromUserQueryHandler : IQueryHandler<GetTracksFromUserQuery, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserInfoService _userInfo;
    private readonly TrackRateService _trackRateService;

    public GetTracksFromUserQueryHandler(
        TrackDbContext context, 
        UserInfoService userInfo,
        TrackRateService trackRateService)
    {
        _context = context;
        _userInfo = userInfo;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(GetTracksFromUserQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _context.MusicTracks
            .Include(track => track.Genres)
            .Where(track => track.UserId == request.UserId && track.Id != request.ExcludeTrackId)
            .OrderByDescending(track => track.UploadDate)
            .ToTracksInfo(request.Take, request.Skip);

        var user = tracks.FirstOrDefault()?.UserId is null ? 
            null : 
            await _userInfo.GetUserInfoAsync(tracks.FirstOrDefault()!.UserId);

        var rates = await _trackRateService.GetTracksRate(tracks.Select(track => track.Id));

        return tracks.Select(trackInfo => new FullTrackInfo
        {
            TrackInfo = trackInfo,
            UserInfo = user,
            Rate = rates?.FirstOrDefault(rate => rate.TrackId == trackInfo.Id)
        });
    }
}