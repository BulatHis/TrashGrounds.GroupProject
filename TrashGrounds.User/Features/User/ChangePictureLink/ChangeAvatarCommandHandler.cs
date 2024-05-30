using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.ChangePictureLink;

public class ChangeAvatarCommandHandler : ICommandHandler<ChangeAvatarCommand, ChangeAvatarResponse>
{
    private readonly IUserDbContext _context;

    public ChangeAvatarCommandHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<ChangeAvatarResponse> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == request.UserId,
                       cancellationToken: cancellationToken)
                   ?? throw new NotFoundException<DomainUser>();

        user.AvatarId = request.NewAvatarId;
        
        await _context.SaveEntitiesAsync();

        return new ChangeAvatarResponse(user.AvatarId);
    }
}