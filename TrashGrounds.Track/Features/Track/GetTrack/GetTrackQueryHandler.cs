using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.GetTrack;

public class GetTrackQueryHandler : IQueryHandler<GetTrackQuery, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly UserInfoService _userInfo;
    private readonly TrackRateService _trackRateService;

    public GetTrackQueryHandler(
        TrackDbContext context, 
        UserInfoService userInfo,
        TrackRateService trackRateService)
    {
        _context = context;
        _userInfo = userInfo;
        _trackRateService = trackRateService;
    }

    public async Task<FullTrack> Handle(GetTrackQuery request, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks
            .Include(track => track.Genres)
            .FirstOrNotFoundAsync(track => track.Id == request.Id,
            cancellationToken: cancellationToken);

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userInfo.GetUserInfoAsync(track.UserId),
            Rate = await _trackRateService.GetTrackRate(track.Id)
        };
    }
}