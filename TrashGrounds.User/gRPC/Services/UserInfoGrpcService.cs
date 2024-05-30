using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Infrastructure;
using UserServer;

namespace TrashGrounds.User.gRPC.Services;

public class UserInfoGrpcService : UserMicroservice.UserMicroserviceBase
{
    private readonly UserDbContext _context;

    public UserInfoGrpcService(UserDbContext context)
    {
        _context = context;
    }

    public override async Task<UserInfoReply> GetUserInfo(UserInfoRequest request, ServerCallContext context)
    {
        var guidId = Guid.Parse(request.Id);
        
        var info = await _context.DomainUsers.Select(user => new
        {
            user.Id,
            user.Nickname,
            user.RegistrationDate,
            user.AvatarId
        }).FirstOrNotFoundAsync(user => user.Id == guidId);

        return new UserInfoReply
        {
            User = new UserInfo
            {
                Id = info.Id.ToString(),
                Nickname = info.Nickname,
                AvatarId = info.AvatarId?.ToString(),
                RegistrationDate = Timestamp.FromDateTime(info.RegistrationDate)
            }
        };
    }
    
    public override async Task<UsersInfoReply> GetUsersInfo(UsersInfoRequest request, ServerCallContext context)
    {
        var guidIds = request.Ids.Select(Guid.Parse).ToList();
        
        var infos = await _context.DomainUsers
            .Where(user => guidIds.Contains(user.Id))
            .Select(user => new
            {
                user.Id,
                user.Nickname,
                user.RegistrationDate,
                user.AvatarId
            }).ToListAsync();

        var reply = new UsersInfoReply();
        
        reply.Users.Add(infos.Select(info =>
            new UserInfo
            {
                Id = info.Id.ToString(),
                Nickname = info.Nickname,
                AvatarId = info.AvatarId?.ToString(),
                RegistrationDate = Timestamp.FromDateTime(info.RegistrationDate)
            }));

        return reply;
    }
}