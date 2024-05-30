using TrashGrounds.File.Features.Image.GetImage;
using TrashGrounds.File.Features.Image.UploadImage;
using TrashGrounds.File.Infrastructure.Routing;

namespace TrashGrounds.File.Features.Image;

public class ImageEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/file/image")
            .WithTags("Изображения")
            .AddEndpoint<UploadImageEndpoint>()
            .AddEndpoint<GetImageEndpoint>();
    }
}