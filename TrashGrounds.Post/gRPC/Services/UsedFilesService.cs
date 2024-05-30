using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Post.Database.Postgres;
using UsedFilesServer;

namespace TrashGrounds.Post.gRPC.Services;

public class UsedFilesService : UsedFilesServer.UsedFilesService.UsedFilesServiceBase
{
    private readonly PostDbContext _context;

    public UsedFilesService(PostDbContext context)
    {
        _context = context;
    }

    public override async Task<UsedImagesResponse> GetUsedImagesIds(UsedImagesRequest request, ServerCallContext context)
    {
        var response = new UsedImagesResponse();
        
        response.Ids.AddRange(
            await _context.Posts
                .Where(post => post.AssetId != null)
                .Select(post => post.AssetId.ToString())
                .ToListAsync());

        return response;
    }
}