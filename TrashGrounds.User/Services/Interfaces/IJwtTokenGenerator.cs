using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Services.Interfaces;

public interface IJwtTokenGenerator
{
    public string GenerateUserToken(DomainUser user, IEnumerable<string> roles, DateTime expiration);
    
    string GenerateFromClaims(IEnumerable<Claim> claims, DateTime expiresAt);
    
    string GenerateToken<T>(T data, DateTime expiresAt);
    
    T? ReadToken<T>(string token, bool shouldThrow = false, TokenValidationParameters? validationParameters = null);
    
    ClaimsPrincipal ReadToken(string token, TokenValidationParameters? validationParameters = null);
    
    TokenValidationParameters CloneParameters();

    RefreshToken GenerateRefreshToken();
    
}