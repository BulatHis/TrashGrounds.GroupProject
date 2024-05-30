using TrashGrounds.User.Infrastructure.Mediator.Command;

namespace TrashGrounds.User.Features.User.ChangeStatus;

public record ChangeStatusCommand(Guid UserId, string NewStatus) : ICommand<ChangeStatusResponse>;