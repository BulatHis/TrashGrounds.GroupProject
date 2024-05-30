using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public class AddTrackCommandHandler : ICommandHandler<AddTrackCommand, FullTrack>
{
    private readonly TrackDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UserInfoService _userInfo;

    public AddTrackCommandHandler(TrackDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        UserInfoService userInfo)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _userInfo = userInfo;
    }

    public async Task<FullTrack> Handle(AddTrackCommand request, CancellationToken cancellationToken)
    {
        var track = new MusicTrack
        {
            Title = request.Title,
            Description = request.Description,
            IsExplicit = request.IsExplicit,
            ListensCount = 0,
            MusicId = request.MusicId,
            PictureId = request.PictureId,
            UploadDate = _dateTimeProvider.UtcNow,
            UserId = request.UserId,
            Genres = await _context.Genres.Where(genre => request.Genres.Contains(genre.Id))
                .ToListAsync(cancellationToken: cancellationToken)
        };
        
        if (!track.Genres.Any())
            throw new NotFoundException<Models.Main.Genre>();

        _context.MusicTracks.Add(track);
        await _context.SaveEntitiesAsync();

        return new FullTrack
        {
            Track = track,
            UserInfo = await _userInfo.GetUserInfoAsync(track.UserId),
            Rate = null
        };
    }
}