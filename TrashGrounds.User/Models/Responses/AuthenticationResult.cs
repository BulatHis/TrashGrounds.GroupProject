using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Models.Responses;

public record AuthenticationResult(string Token, RefreshToken RefreshToken);