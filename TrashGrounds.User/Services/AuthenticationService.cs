using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Constants;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;
using TrashGrounds.User.Services.Configs;
using TrashGrounds.User.Services.Interfaces;

namespace TrashGrounds.User.Services;

public class AuthenticationService
{
    private readonly IUserDbContext _dbContext;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;


    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IUserDbContext dbContext)
    {
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
        _signInManager = signInManager;
        _userManager = userManager;

    }

    public async Task<AuthenticationResult> AuthenticateUser(AppUser identityUser, DomainUser domainUser)
    {
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
        var roles = await _userManager.GetRolesAsync(identityUser);
        var token = _jwtTokenGenerator.GenerateUserToken(domainUser, roles,
            DateTime.UtcNow.AddSeconds(TokensConfig.TokenLifetime));

        identityUser.AddRefreshToken(refreshToken);

        return new AuthenticationResult(token, refreshToken);
    }
    
    public async Task<AuthenticationResult> ProcessPasswordLogin(string email, string password)
    {
        var identityUser = await _userManager.FindByEmailAsync(email)
                           ?? throw new NotFoundException<AppUser>();
        
        var domainUser = await _dbContext.DomainUsers.FirstOrDefaultAsync(x => x.IdentityUserId == identityUser.Id)
                         ?? throw new NotFoundException<DomainUser>();
        
        var result = await _signInManager.CheckPasswordSignInAsync(identityUser, password, false);

        if (!result.Succeeded)
            throw new ValidationFailedException("Password", ValidationFailedMessages.WrongPassword);

        return await AuthenticateUser(identityUser, domainUser);
    }
}