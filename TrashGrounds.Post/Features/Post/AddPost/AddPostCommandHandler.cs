using TrashGrounds.Post.Database.Postgres;
using TrashGrounds.Post.Infrastructure.Mediator.Command;
using TrashGrounds.Post.Services.Interfaces;

namespace TrashGrounds.Post.Features.Post.AddPost;

public class AddPostCommandHandler : ICommandHandler<AddPostCommand, Models.Main.Post>
{
    private readonly PostDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddPostCommandHandler(PostDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Models.Main.Post> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Models.Main.Post
        {
            Text = request.Text,
            UploadDate = _dateTimeProvider.UtcNow,
            UserId = request.UserId,
            AssetId = request.AssetId,
            IsHidden = request.IsHidden
        };

        _context.Posts.Add(post);

        await _context.SaveEntitiesAsync();

        return post;
    }
}