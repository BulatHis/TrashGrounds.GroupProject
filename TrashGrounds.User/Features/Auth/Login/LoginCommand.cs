using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.Login;

public record LoginCommand(string Email, string Password) : ICommand<AuthorizationResponse>;