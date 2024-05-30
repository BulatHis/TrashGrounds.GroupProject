using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using UsedFilesServer;

namespace TrashGrounds.Track.gRPC.Services;

public class UsedFilesService : UsedFilesServer.UsedFilesService.UsedFilesServiceBase
{
    private readonly TrackDbContext _context;

    public UsedFilesService(TrackDbContext context)
    {
        _context = context;
    }

    public override async Task<UsedImagesResponse> GetUsedImagesIds(UsedImagesRequest request, ServerCallContext context)
    {
        var response = new UsedImagesResponse();

        response.Ids.AddRange(
            await _context.MusicTracks
                .Where(track => track.PictureId != null)
                .Select(track => track.PictureId.ToString())
                .ToListAsync());

        return response;
    }

    public override async Task<UsedTracksResponse> GetUsedTracksIds(UsedTracksRequest request, ServerCallContext context)
    {
        var response = new UsedTracksResponse();

        response.Ids.AddRange(
            await _context.MusicTracks
                .Select(track => track.MusicId.ToString())
                .ToListAsync());

        return response;
    }
}