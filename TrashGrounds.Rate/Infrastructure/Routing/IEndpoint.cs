namespace TrashGrounds.Rate.Infrastructure.Routing;

public interface IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints);
}