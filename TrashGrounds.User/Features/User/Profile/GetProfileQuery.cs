using TrashGrounds.User.Infrastructure.Mediator.Query;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Features.User.Profile;

public record GetProfileQuery(Guid UserId) : IQuery<UserProfile>;