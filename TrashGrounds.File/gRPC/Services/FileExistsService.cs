using FileServer;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.File.Database.Postgres;

namespace TrashGrounds.File.gRPC.Services;

public class FileExistsService : FileService.FileServiceBase
{
    private readonly FileDbContext _context;

    public FileExistsService(FileDbContext context)
    {
        _context = context;
    }

    public override async Task<ImageResponse> CheckImageExists(ImageRequest request, ServerCallContext context)
    {
        return new ImageResponse
        {
            Exists = await _context.ImageFiles.AnyAsync(image => image.Id == Guid.Parse(request.ImageId))
        };
    }

    public override async Task<TrackResponse> CheckTrackExists(TrackRequest request, ServerCallContext context)
    {
        return new TrackResponse
        {
            Exists = await _context.MusicFiles.AnyAsync(music => music.Id == Guid.Parse(request.TrackId))
        };
    }
}