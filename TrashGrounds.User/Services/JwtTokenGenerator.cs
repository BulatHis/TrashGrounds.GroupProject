using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Services.Configs;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly IConfiguration _configuration;
    private readonly TokenValidationParameters _defaultValidationParameters;
    
    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //TODO вынести параметры валидации
        _defaultValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key")!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }

    public string GenerateUserToken(DomainUser user, IEnumerable<string> roles, DateTime expiration)
        => GenerateFromClaims(roles
                .Select(role => new Claim(ClaimTypes.Role, role))
                .Append(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())),
            expiration);

    public string GenerateFromClaims(IEnumerable<Claim> claims, DateTime expiresAt)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = expiresAt,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key")!)),
                    SecurityAlgorithms.HmacSha256Signature)
        };
        var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    public string GenerateToken<T>(T data, DateTime expiresAt)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(typeof(T).Name, JsonSerializer.Serialize(data))
            }),
            Expires = expiresAt,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key")!)), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    public T? ReadToken<T>(string token, bool shouldThrow = false, TokenValidationParameters? validationParameters = null)
    {
        try
        {
            var principal = ReadToken(token, validationParameters);
            var claim = principal.Claims.FirstOrDefault(c => c.Type == typeof(T).Name);
            return JsonSerializer.Deserialize<T>(claim!.Value);    
        }
        catch (Exception e)
        {
            if (shouldThrow)
            {
                throw;
            }
            return default;
        }
    }
    
    public ClaimsPrincipal ReadToken(string token, TokenValidationParameters? validationParameters = null) 
        => _jwtSecurityTokenHandler.ValidateToken(
            token, 
            validationParameters ?? _defaultValidationParameters, 
            out _);

    public TokenValidationParameters CloneParameters()
        => _defaultValidationParameters.Clone();
    
    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken(
            Convert.ToBase64String(randomNumber),
            DateTime.UtcNow,
            DateTime.UtcNow.AddSeconds(TokensConfig.RefreshTokenLifetime));
    }
}