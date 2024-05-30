using TrashGrounds.Comment.Database.Postgres;
using TrashGrounds.Comment.Infrastructure;
using TrashGrounds.Comment.Infrastructure.Exceptions;
using TrashGrounds.Comment.Infrastructure.Mediator.Command;

namespace TrashGrounds.Comment.Features.Comment.DeleteComment;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, bool>
{
    private readonly CommentDbContext _context;

    public DeleteCommentCommandHandler(CommentDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrNotFoundAsync(
            comment => comment.TrackId == request.TrackId && comment.Id == request.CommentId,
            cancellationToken: cancellationToken);

        if (comment.UserId != request.CurrentUserId)
            throw new ForbiddenException("Not your comment");

        _context.Comments.Remove(comment);

        await _context.SaveEntitiesAsync();
        
        return true;
    }
}