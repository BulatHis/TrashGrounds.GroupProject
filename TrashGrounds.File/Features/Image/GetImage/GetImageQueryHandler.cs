using TrashGrounds.File.Database.Postgres;
using TrashGrounds.File.Extensions;
using TrashGrounds.File.Infrastructure;
using TrashGrounds.File.Infrastructure.Exceptions;
using TrashGrounds.File.Infrastructure.Mediator.Query;
using TrashGrounds.File.Models.Additional;
using TrashGrounds.File.Models.Main;

namespace TrashGrounds.File.Features.Image.GetImage;

public class GetImageQueryHandler : IQueryHandler<GetImageQuery, FileResponse>
{
    private readonly FileDbContext _context;

    public GetImageQueryHandler(FileDbContext context)
    {
        _context = context;
    }

    public async Task<FileResponse> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var imageFile = await _context.ImageFiles.FirstOrNotFoundAsync(file => file.Id == request.FileId,
            cancellationToken: cancellationToken);

        var filePath = imageFile.Route;

        if (!System.IO.File.Exists(filePath))
            throw new NotFoundException<MusicFile>();

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        memory.Position = 0;

        return new FileResponse(memory, ContentTypes.GetType(filePath), imageFile.Id);
    }
}