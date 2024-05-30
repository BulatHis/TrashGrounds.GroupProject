using TrashGrounds.Comment.Infrastructure.Mediator.Command;

namespace TrashGrounds.Comment.Features.Comment.AddComment;

public record AddCommentCommand(
    Guid UserId, 
    Guid TrackId, 
    string Message, 
    Guid? ReplyTo) : ICommand<Models.Main.Comment>;