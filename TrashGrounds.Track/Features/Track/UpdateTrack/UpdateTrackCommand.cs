using TrashGrounds.Track.Infrastructure.Mediator.Command;
using TrashGrounds.Track.Models.Additional;

namespace TrashGrounds.Track.Features.Track.UpdateTrack;

public record UpdateTrackCommand(
    Guid UserId,
    Guid TrackId,
    string Title,
    string? Description,
    bool IsExplicit,
    IEnumerable<Guid> Genres,
    Guid? NewPictureId,
    bool ChangePicture,
    Guid? NewMusicId,
    bool ChangeMusic) : ICommand<FullTrack>;