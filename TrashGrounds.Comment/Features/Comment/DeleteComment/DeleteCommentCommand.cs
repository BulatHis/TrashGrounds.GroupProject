using TrashGrounds.Comment.Infrastructure.Mediator.Command;

namespace TrashGrounds.Comment.Features.Comment.DeleteComment;

public record DeleteCommentCommand(Guid CurrentUserId, Guid TrackId, Guid CommentId) : ICommand<bool>;