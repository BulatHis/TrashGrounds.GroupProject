using MediatR;
using TrashGrounds.Track.Infrastructure.Routing;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Features.Track.DeleteTrack;

public class DeleteTrackEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/{trackId:guid}",
                async (Guid trackId, IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new DeleteTrackCommand(
                        userService.GetUserIdOrThrow(),
                        trackId))))
            .RequireAuthorization();
    }
}