namespace TrashGrounds.File.Models.Additional;

public record FileResponse(MemoryStream Stream, string ContentType, Guid FileId);