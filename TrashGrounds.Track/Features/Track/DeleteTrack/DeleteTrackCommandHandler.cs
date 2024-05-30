using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.DeleteTrack;

public class DeleteTrackCommandHandler : ICommandHandler<DeleteTrackCommand, SuccessResponse>
{
    private readonly TrackDbContext _context;

    public DeleteTrackCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == request.TrackId,
            cancellationToken);
        
        if (track.UserId != request.UserId)
            throw new ForbiddenException("Can't delete not your track");
        
        _context.MusicTracks.Remove(track);
        await _context.SaveEntitiesAsync();

        return new SuccessResponse("Deleted");
    }
}