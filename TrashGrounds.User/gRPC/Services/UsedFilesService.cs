using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using UsedFilesServer;

namespace TrashGrounds.User.gRPC.Services;

public class UsedFilesService : UsedFilesServer.UsedFilesService.UsedFilesServiceBase
{
    private readonly UserDbContext _context;

    public UsedFilesService(UserDbContext context)
    {
        _context = context;
    }

    public override async Task<UsedImagesResponse> GetUsedImagesIds(UsedImagesRequest request, ServerCallContext context)
    {
        var response = new UsedImagesResponse();

        response.Ids.AddRange(
            await _context.DomainUsers
                .Where(user => user.AvatarId != null)
                .Select(post => post.AvatarId.ToString())
                .ToListAsync());

        return response;
    }
}