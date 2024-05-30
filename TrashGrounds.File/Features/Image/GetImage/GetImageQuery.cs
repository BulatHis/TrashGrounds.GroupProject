using TrashGrounds.File.Infrastructure.Mediator.Query;
using TrashGrounds.File.Models.Additional;

namespace TrashGrounds.File.Features.Image.GetImage;

public record GetImageQuery(Guid FileId) : IQuery<FileResponse>;