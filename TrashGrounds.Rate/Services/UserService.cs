using System.Security.Claims;
using TrashGrounds.Rate.Infrastructure.Exceptions;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    private Guid? UserId => Guid.TryParse(
        _httpContextAccessor.HttpContext
            ?.User
            ?.FindFirstValue(ClaimTypes.NameIdentifier),
        out var userId)
        ? userId
        : null;

    public Guid GetUserIdOrThrow() => UserId ?? throw new UnauthorizedException("Unauthorized");
}