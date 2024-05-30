using System.Security.Claims;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Services;


public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private Guid? UserId => Guid.TryParse(
        _httpContextAccessor.HttpContext
            ?.User
            ?.FindFirstValue(ClaimTypes.NameIdentifier),
        out var userId)
        ? userId
        : null;

    public Guid GetUserIdOrThrow() => UserId ?? throw new UnauthorizedException("Unauthorized");

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}