using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.AddTrack;

public record AddTrackCommand(
    Guid UserId,
    string Title,
    string? Description,
    bool IsExplicit,
    Guid? PictureId,
    Guid MusicId,
    IEnumerable<Guid> Genres) : ICommand<FullTrack>;