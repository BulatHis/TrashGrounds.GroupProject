using MediatR;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;

namespace TrashGrounds.User.Features.Auth.Register;

public class RegisterEndpoint : IEndpoint
{
    public record RegisterDto(string Email, string Nickname, string Password);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/register", async (RegisterDto dto, IMediator mediator) =>
                Results.Ok(await mediator.Send(new RegisterCommand(
                    dto.Email,
                    dto.Nickname,
                    dto.Password))))
            .AddValidation(builder => builder.AddFor<RegisterDto>());
    }
}