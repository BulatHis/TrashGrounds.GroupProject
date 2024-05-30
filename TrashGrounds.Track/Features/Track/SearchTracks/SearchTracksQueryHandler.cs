using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Mediator.Query;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.SearchTracks;

public class SearchTracksQueryHandler : IQueryHandler<SearchTracksQuery, IEnumerable<FullTrackInfo>>
{
    private readonly TrackDbContext _context;
    private readonly UserInfoService _userInfo;
    private readonly TrackRateService _trackRateService;

    public SearchTracksQueryHandler(
        TrackDbContext context, 
        UserInfoService userInfo,
        TrackRateService trackRateService)
    {
        _context = context;
        _userInfo = userInfo;
        _trackRateService = trackRateService;
    }

    public async Task<IEnumerable<FullTrackInfo>> Handle(SearchTracksQuery request, CancellationToken cancellationToken)
    {
        var tracks = await _context.MusicTracks
            .Include(track => track.Genres)
            .Where(track =>
                request.Query == null || track.Title.ToLower().Contains(request.Query.ToLower()))
            /*.Where(track => command.GenresId == null || !command.GenresId.Any() ||
                            track.Genres.Select(genre => genre.Id).Intersect(command.GenresId).Count() ==
                            command.GenresId.Count())*/ //TODO не работает, переделать, с ALL тоже не работает
            .ToTracksInfo(request.Take, request.Skip);
        
        return tracks.ToFullTrackInfo(
            await _userInfo.GetUsersInfoAsync(tracks.Select(track => track.UserId)),
            await _trackRateService.GetTracksRate(tracks.Select(track => track.Id)));
    }
}