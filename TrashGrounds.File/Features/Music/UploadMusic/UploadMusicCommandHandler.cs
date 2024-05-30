using TrashGrounds.File.Database.Postgres;
using TrashGrounds.File.Infrastructure.Mediator.Command;
using TrashGrounds.File.Models.Main;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Features.Music.UploadMusic;

public class UploadMusicCommandHandler : ICommandHandler<UploadMusicCommand, Guid>
{
    private readonly FileDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UploadMusicCommandHandler(FileDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(UploadMusicCommand request, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(StoragePaths.MusicPath,
            $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}");
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var musicFile = new MusicFile
        {
            Route = filePath,
            UploadDate = DateTime.UtcNow,
            UserId = request.UserId
        };

        _context.MusicFiles.Add(musicFile);
        await _context.SaveEntitiesAsync();

        return musicFile.Id;
    }
}