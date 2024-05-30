using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.RefreshTokens;

public record RefreshTokensCommand(string Token, string RefreshToken) : ICommand<RefreshTokenResponse>;