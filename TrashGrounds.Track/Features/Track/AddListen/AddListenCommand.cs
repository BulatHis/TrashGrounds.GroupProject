using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddListen;

public record AddListenCommand(Guid TrackId) : ICommand<SuccessResponse>;