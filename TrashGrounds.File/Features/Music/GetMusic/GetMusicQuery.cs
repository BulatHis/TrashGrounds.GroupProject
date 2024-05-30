using TrashGrounds.File.Infrastructure.Mediator.Query;
using TrashGrounds.File.Models.Additional;

namespace TrashGrounds.File.Features.Music.GetMusic;

public record GetMusicQuery(Guid FileId) : IQuery<FileResponse>;