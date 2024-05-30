using TrashGrounds.Comment.Database.Postgres;
using TrashGrounds.Comment.Infrastructure;
using TrashGrounds.Comment.Infrastructure.Mediator.Command;
using TrashGrounds.Comment.Services.Interfaces;

namespace TrashGrounds.Comment.Features.Comment.AddComment;

public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand, Models.Main.Comment>
{
    private readonly CommentDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddCommentCommandHandler(CommentDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Models.Main.Comment> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        if (request.ReplyTo is not null)
        {
            await _context.Comments.AnyOrNotFoundAsync(comment => comment.Id == request.ReplyTo,
                cancellationToken: cancellationToken);
        }

        var comment = new Models.Main.Comment
        {
            Message = request.Message,
            SendAt = _dateTimeProvider.UtcNow,
            EditedAt = null,
            ReplyTo = request.ReplyTo,
            TrackId = request.TrackId,
            UserId = request.UserId
        };

        _context.Comments.Add(comment);

        await _context.SaveEntitiesAsync();

        return comment;
    }
}