using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure;
using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddListen;

public class AddListenCommandHandler : ICommandHandler<AddListenCommand, SuccessResponse>
{
    private readonly TrackDbContext _context;

    public AddListenCommandHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<SuccessResponse> Handle(AddListenCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.MusicTracks.FirstOrNotFoundAsync(track => track.Id == request.TrackId,
            cancellationToken: cancellationToken);

        track.ListensCount++;

        await _context.SaveEntitiesAsync();

        return new SuccessResponse();
    }
}