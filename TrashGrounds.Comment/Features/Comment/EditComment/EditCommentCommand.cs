using TrashGrounds.Comment.Infrastructure.Mediator.Command;

namespace TrashGrounds.Comment.Features.Comment.EditComment;

public record EditCommentCommand(
    Guid CurrentUserId,
    Guid TrackId,
    Guid CommentId, 
    string Message,
    Guid? ReplyTo) : ICommand<Models.Main.Comment>;