using TrashGrounds.File.Infrastructure.Mediator.Command;

namespace TrashGrounds.File.Features.Image.UploadImage;

public record UploadImageCommand(IFormFile File, Guid UserId) : ICommand<Guid>;