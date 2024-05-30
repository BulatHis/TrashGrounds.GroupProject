using MediatR;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public record LoginDto(string Email, string Password);

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login", async (LoginDto dto, IMediator mediator) =>
                Results.Ok(await mediator.Send(new LoginCommand(
                    dto.Email,
                    dto.Password))))
            .AddValidation(builder => builder.AddFor<LoginDto>());
    }
}