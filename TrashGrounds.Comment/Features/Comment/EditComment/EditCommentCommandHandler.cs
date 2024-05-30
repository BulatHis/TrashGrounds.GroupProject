using TrashGrounds.Comment.Database.Postgres;
using TrashGrounds.Comment.Infrastructure;
using TrashGrounds.Comment.Infrastructure.Exceptions;
using TrashGrounds.Comment.Infrastructure.Mediator.Command;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.EditComment;

public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand, Models.Main.Comment>
{
    private readonly CommentDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public EditCommentCommandHandler(CommentDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Models.Main.Comment> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrNotFoundAsync(
            comment => comment.TrackId == request.TrackId && comment.Id == request.CommentId,
            cancellationToken: cancellationToken);

        if (comment.UserId != request.CurrentUserId)
            throw new ForbiddenException("Not your comment");

        if (request.ReplyTo is not null && request.ReplyTo != comment.ReplyTo)
        {
            await _context.Comments.AnyOrNotFoundAsync(c => c.Id == request.ReplyTo,
                cancellationToken: cancellationToken);

            comment.ReplyTo = request.ReplyTo;
        }

        comment.Message = request.Message;
        comment.EditedAt = _dateTimeProvider.UtcNow;

        await _context.SaveEntitiesAsync();

        return comment;
    }
}