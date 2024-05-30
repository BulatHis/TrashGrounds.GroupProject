using Microsoft.EntityFrameworkCore;
using TrashGrounds.User.Database.Postgres.Interfaces;
using TrashGrounds.User.Infrastructure.Exceptions;
using TrashGrounds.User.Infrastructure.Mediator.Command;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.ChangeStatus;

public class ChangeStatusEndpointHandler : ICommandHandler<ChangeStatusCommand, ChangeStatusResponse>
{
    private readonly IUserDbContext _context;

    public ChangeStatusEndpointHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<ChangeStatusResponse> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FirstOrDefaultAsync(user => user.Id == command.UserId,
                       cancellationToken: cancellationToken)
                   ?? throw new NotFoundException<DomainUser>();

        user.Status = command.NewStatus;
        
        await _context.SaveEntitiesAsync();

        return new ChangeStatusResponse(user.Status);
    }
}