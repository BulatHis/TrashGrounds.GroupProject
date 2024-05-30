namespace TrashGrounds.User.Models.Responses;

public record AuthorizationResponse(string AccessToken, string RefreshToken)
{
    public static AuthorizationResponse FromAuthenticationResult(AuthenticationResult authenticationResult)
    {
        return new AuthorizationResponse(authenticationResult.Token, authenticationResult.RefreshToken.Token);
    }
}