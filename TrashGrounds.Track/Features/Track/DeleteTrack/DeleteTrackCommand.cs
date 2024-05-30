using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.DeleteTrack;

public record DeleteTrackCommand(Guid UserId, Guid TrackId) : ICommand<SuccessResponse>;