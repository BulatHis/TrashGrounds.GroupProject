using TrashGrounds.File.Infrastructure.Mediator.Command;

namespace TrashGrounds.File.Features.Music.UploadMusic;

public record UploadMusicCommand(IFormFile File, Guid UserId) : ICommand<Guid>;