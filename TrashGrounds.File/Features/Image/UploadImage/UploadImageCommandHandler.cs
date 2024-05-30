using TrashGrounds.File.Database.Postgres;
using TrashGrounds.File.Infrastructure.Mediator.Command;
using TrashGrounds.File.Models.Main;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Features.Image.UploadImage;

public class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, Guid>
{
    private readonly FileDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UploadImageCommandHandler(FileDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        // var filePath = Path.Combine(StoragePaths.ImagePath,
        //     $"{Guid.NewGuid()}_{_dateTimeProvider.UtcNow.ToShortDateString()}{Path.GetExtension(request.File.FileName)}");
        var filePath = Path.Combine(StoragePaths.ImagePath,
            $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}");
        
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var imageFile = new ImageFile
        {
            Route = filePath,
            UploadDate = DateTime.UtcNow,
            UserId = request.UserId
        };

        _context.ImageFiles.Add(imageFile);
        await _context.SaveEntitiesAsync();

        return imageFile.Id;
    }
}