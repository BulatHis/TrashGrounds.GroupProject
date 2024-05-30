using TrashGrounds.Rate.Infrastructure.Mediator.Command;

namespace TrashGrounds.Rate.Features.Post.DeletePostRate;

public record DeletePostRateCommand(Guid UserId, Guid PostId) : ICommand<bool>;