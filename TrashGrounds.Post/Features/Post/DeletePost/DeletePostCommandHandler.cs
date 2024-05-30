using TrashGrounds.Post.Database.Postgres;
using TrashGrounds.Post.Infrastructure;
using TrashGrounds.Post.Infrastructure.Exceptions;
using TrashGrounds.Post.Infrastructure.Mediator.Command;

namespace TrashGrounds.Post.Features.Post.DeletePost;

public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, bool>
{
    private readonly PostDbContext _context;

    public DeletePostCommandHandler(PostDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrNotFoundAsync(post => post.Id == request.PostId,
            cancellationToken: cancellationToken);

        if (post.UserId != request.CurrentUserId)
            throw new ForbiddenException("Not your post");

        _context.Posts.Remove(post);

        await _context.SaveEntitiesAsync();
        
        return true;
    }
}