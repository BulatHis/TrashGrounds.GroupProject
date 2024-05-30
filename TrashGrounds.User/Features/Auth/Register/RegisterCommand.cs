using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.Register;

public record RegisterCommand(string Email, string Nickname, string Password) : ICommand<AuthorizationResponse>;