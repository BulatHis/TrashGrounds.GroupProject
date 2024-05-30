using TrashGrounds.User.Features.User.ChangePictureLink;
using TrashGrounds.User.Features.User.ChangeStatus;
using TrashGrounds.User.Features.User.Me;
using TrashGrounds.User.Features.User.Profile;
using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.User;

public class UserEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/user")
            .WithTags("Пользователь")
            .AddEndpoint<GetProfileEndpoint>()
            .AddEndpoint<ChangeAvatarEndpoint>()
            .AddEndpoint<ChangeStatusEndpoint>()
            .AddEndpoint<MeEndpoint>();
    }
}