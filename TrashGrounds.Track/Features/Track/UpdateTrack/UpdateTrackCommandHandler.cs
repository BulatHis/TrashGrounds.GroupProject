using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public class UpdateTrackCommandHandler : ICommandHandler<UpdateTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly UserInfoService _userInfo;
    private readonly TrackRateService _trackRateService;

    public UpdateTrackCommandHandler(
        TrackDbContext context,
        UserInfoService userInfo,
        TrackRateService trackRateService)
    {
        _context = context;
        _userInfo = userInfo;
        _trackRateService = trackRateService;
    }

    public async Task<FullTrack> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks
            .Include(track => track.Genres)
            .FirstOrNotFoundAsync(track => track.Id == request.TrackId, cancellationToken);

        if (track.UserId != request.UserId)
            throw new ForbiddenException("Can't change not your track");

        var genres = await _context.Genres.Where(genre => request.Genres.Contains(genre.Id))
            .ToListAsync(cancellationToken: cancellationToken);
        if (!track.Genres.Any())
            throw new NotFoundException<Models.Main.Genre>();

        track.Genres = genres;
        track.Title = request.Title;
        track.Description = request.Description;
        track.IsExplicit = request.IsExplicit;

        if (request.ChangePicture)
            track.PictureId = request.NewPictureId;
        if (request.ChangeMusic)
            track.MusicId = request.NewMusicId!.Value;

        await _context.SaveEntitiesAsync();

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userInfo.GetUserInfoAsync(track.UserId),
            Rate = await _trackRateService.GetTrackRate(track.Id)
        };
    }
}