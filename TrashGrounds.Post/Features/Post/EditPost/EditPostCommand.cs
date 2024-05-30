using TrashGrounds.Post.Infrastructure.Mediator.Command;

namespace TrashGrounds.Post.Features.Post.EditPost;

public record EditPostCommand(
    Guid CurrentUserId,
    Guid PostId,
    string Text,
    Guid? AssetId,
    bool ChangeAssetId,
    bool IsHidden) : ICommand<Models.Main.Post>;