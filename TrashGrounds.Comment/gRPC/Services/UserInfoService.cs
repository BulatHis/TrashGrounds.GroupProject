using TrashGrounds.Comment.Models.Additional;
using UserClient;

namespace TrashGrounds.Comment.gRPC.Services;

public class UserInfoService
{
    private readonly UserMicroservice.UserMicroserviceClient _userClient;

    public UserInfoService(UserMicroservice.UserMicroserviceClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<UserInformation?> GetUserInfoAsync(Guid id)
    {
        try
        {
            var info = await _userClient.GetUserInfoAsync(new UserInfoRequest {Id = id.ToString()});
            
            return info?.User is null
                ? null
                : new UserInformation
                {
                    Id = Guid.Parse(info.User.Id),
                    Nickname = info.User.Nickname,
                    AvatarId = info.User.AvatarId is null ? null : Guid.Parse(info.User.AvatarId),
                    RegistrationDate = info.User.RegistrationDate.ToDateTime()
                };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<UserInformation>?> GetUsersInfoAsync(IEnumerable<Guid> ids)
    {
        try
        {
            var request = new UsersInfoRequest();
            request.Ids.Add(ids.Select(guid => guid.ToString()));

            var infos = await _userClient.GetUsersInfoAsync(request);

            return infos?.Users?.Select(user => new UserInformation
            {
                Id = Guid.Parse(user.Id),
                Nickname = user.Nickname,
                AvatarId = user.AvatarId is null ? null : Guid.Parse(user.AvatarId),
                RegistrationDate = user.RegistrationDate.ToDateTime()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}