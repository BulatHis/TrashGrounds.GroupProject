using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;
using TrashGrounds.User.Services;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Features.Auth.RefreshTokens;

public class RefreshTokensCommandHandler : ICommandHandler<RefreshTokensCommand, RefreshTokenResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly UserDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AuthenticationService _authService;

    public RefreshTokensCommandHandler(UserManager<AppUser> userManager, UserDbContext context, IJwtTokenGenerator jwtTokenGenerator, AuthenticationService authService)
    {
        _userManager = userManager;
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
        _authService = authService;
    }
    
    public async Task<RefreshTokenResponse> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var validationParamsIgnoringTime = _jwtTokenGenerator.CloneParameters();
        validationParamsIgnoringTime.ValidateLifetime = false;

        var userId =
            Guid.Parse(
                _jwtTokenGenerator.ReadToken(request.Token, validationParamsIgnoringTime)
                    .FindFirst(x => x.Type == ClaimTypes.NameIdentifier)
                    ?.Value
                ?? throw new UnauthorizedException("INVALID_OLD_TOKEN"));

        var user = await _context.DomainUsers.FirstOrDefaultAsync(x => x.Id == userId,
                       cancellationToken: cancellationToken)
                   ?? throw new NotFoundException<DomainUser>();

        var applicationUser = await _userManager.FindByIdAsync(user.IdentityUserId.ToString())
                              ?? throw new NotFoundException<AppUser>();
        
        var token = applicationUser.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken)
                    ?? throw new UnauthorizedException("INVALID_REFRESH_TOKEN");

        applicationUser.RemoveRefreshToken(token);
        
        var result = await _authService.AuthenticateUser(applicationUser, user);

        await _context.SaveEntitiesAsync();
        
        return new RefreshTokenResponse(result.Token, result.RefreshToken.Token);
    }
}