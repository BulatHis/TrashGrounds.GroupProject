using TrashGrounds.Post.Infrastructure.Mediator.Command;

namespace TrashGrounds.Post.Features.Post.DeletePost;

public record DeletePostCommand(Guid CurrentUserId, Guid PostId) : ICommand<bool>;