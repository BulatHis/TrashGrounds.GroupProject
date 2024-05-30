using Microsoft.AspNetCore.Identity;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Main;
using TrashGrounds.User.Models.Responses;

namespace TrashGrounds.User.Features.Auth.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, SuccessResponse>
{
    private readonly IUserDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ChangePasswordCommandHandler(IUserDbContext context, 
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<SuccessResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FirstOrNotFoundAsync(user => user.Id == request.UserId,
            cancellationToken: cancellationToken);

        var appUser = await _userManager.FindByIdAsync(user.IdentityUserId.ToString())
                      ?? throw new NotFoundException<AppUser>();

        var result = await _userManager.ChangePasswordAsync(appUser, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
            throw new ValidationFailedException("Password", result.Errors.First().Description);

        return new SuccessResponse();
    }
}