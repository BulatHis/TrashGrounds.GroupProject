using TrashGrounds.User.Features.Auth.AuthCheck;
using TrashGrounds.User.Features.Auth.ChangePassword;
using TrashGrounds.User.Features.Auth.Login;
using TrashGrounds.User.Features.Auth.RefreshTokens;
using TrashGrounds.User.Features.Auth.Register;
using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.Auth;

public class AuthEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/auth")
            .WithTags("Авторизация")
            .AddEndpoint<LoginEndpoint>()
            .AddEndpoint<RegisterEndpoint>()
            .AddEndpoint<RefreshTokensEndpoint>()
            .AddEndpoint<AuthCheckEndpoint>()
            .AddEndpoint<ChangePasswordEndpoint>();
    }
}