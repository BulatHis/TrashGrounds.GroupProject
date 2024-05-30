using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Features.Track.GetTracksByCategory;

public class GetTracksByCategoryQueryHandler : IQueryHandler<GetTracksByCategoryQuery, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserInfoService _userInfo;
    private readonly TrackRateService _trackRateService;

    public GetTracksByCategoryQueryHandler(
        TrackDbContext context, 
        UserInfoService userInfo,
        TrackRateService trackRateService)
    {
        _context = context;
        _userInfo = userInfo;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(GetTracksByCategoryQuery request, CancellationToken cancellationToken)
    {
        return request.Category switch
        {
            Category.New => await GetNewTracks(request.Take, request.Skip),
            Category.MostStreaming => await GetMostStreamingTracks(request.Take, request.Skip),
            Category.Popular => await GetPopularTracks(request.Take, request.Skip),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Category), request.Category, null)
        };
    }

    private async Task<IEnumerable<FullTrackInfo>> GetNewTracks(int take, int skip)
    {
        var tracks = await _context.MusicTracks
            .OrderByDescending(track => track.UploadDate)
            .ToTracksInfo(take, skip);

        return tracks.ToFullTrackInfo(
            await _userInfo.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }

    private async Task<IEnumerable<FullTrackInfo>> GetMostStreamingTracks(int take, int skip)
    {
        var tracks = await _context.MusicTracks
            .OrderByDescending(track => track.ListensCount)
            .ToTracksInfo(take, skip);
        
        return tracks.ToFullTrackInfo(
            await _userInfo.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }
    
    private async Task<IEnumerable<FullTrackInfo>> GetPopularTracks(int take, int skip)
    {
        var rates = await _trackRateService.GetBestRatedTracks(take, skip) ?? new List<Rate>();

        var tracks = await _context.MusicTracks
            .Where(track => rates.Select(rate => rate.TrackId).Contains(track.Id))
            .ToTracksInfo(int.MaxValue);

        return tracks.ToFullTrackInfo(
                await _userInfo.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
                rates)
            .OrderByDescending(info => info.Rate?.Rating);
    }
}