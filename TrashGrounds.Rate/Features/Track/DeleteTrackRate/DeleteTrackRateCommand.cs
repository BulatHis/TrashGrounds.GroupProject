using TrashGrounds.Rate.Infrastructure.Mediator.Command;

namespace TrashGrounds.Rate.Features.Track.DeleteTrackRate;

public record DeleteTrackRateCommand(Guid UserId, Guid TrackId) : ICommand<bool>;