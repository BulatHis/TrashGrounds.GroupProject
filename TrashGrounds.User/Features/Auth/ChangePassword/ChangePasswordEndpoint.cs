using MediatR;
using TrashGrounds.User.Infrastructure.Routing;
using TrashGrounds.User.Infrastructure.ValidationSetup;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.Auth.ChangePassword;

public class ChangePasswordEndpoint : IEndpoint
{
    public record ChangePasswordDto(string OldPassword, string NewPassword);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/change-password",
                async (ChangePasswordDto dto, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new ChangePasswordCommand(
                        userService.GetUserIdOrThrow(),
                        dto.OldPassword,
                        dto.NewPassword))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangePasswordDto>());
    }
}