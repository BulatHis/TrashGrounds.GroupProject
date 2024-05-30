using TrashGrounds.Post.Infrastructure.Mediator.Command;

namespace TrashGrounds.Post.Features.Post.AddPost;

public record AddPostCommand(Guid UserId, string Text, Guid? AssetId, bool IsHidden) : ICommand<Models.Main.Post>;