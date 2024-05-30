using TrashGrounds.Rate.Infrastructure.Mediator.Command;
using TrashGrounds.Rate.Models.Additional.Post;

namespace TrashGrounds.Rate.Features.Post.ChangePostRate;

public record ChangePostRateCommand(Guid UserId, Guid PostId, int NewRate) : ICommand<PostUserRateResponse>;