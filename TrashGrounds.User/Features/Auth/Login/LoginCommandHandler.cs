using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, AuthorizationResponse>
{
    private readonly IUserDbContext _context;
    private readonly Services.AuthenticationService _authService;
    public LoginCommandHandler(IUserDbContext context, Services.AuthenticationService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<AuthorizationResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        //TODO сделать авторизацию по почте или логину, а не только логину
        var result = AuthorizationResponse.FromAuthenticationResult(
            await _authService.ProcessPasswordLogin(
                command.Email
                ?? throw new ValidationFailedException(nameof(command.Email), "EMAIL_IS_NULL"),
                command.Password
                ?? throw new ValidationFailedException(nameof(command.Password), "PASSWORD_IS_NULL")));

        await _context.SaveEntitiesAsync();
        return result;
    }
}