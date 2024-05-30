using MediatR;
using TrashGrounds.User.Infrastructure.Routing;

namespace TrashGrounds.User.Features.User.Profile;

public class GetProfileEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:Guid}",
            async (Guid id, IMediator mediator) =>
                Results.Ok(await mediator.Send(
                    new GetProfileQuery(id))));
    }
}