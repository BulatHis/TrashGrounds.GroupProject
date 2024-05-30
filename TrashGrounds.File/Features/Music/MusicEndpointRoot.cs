using TrashGrounds.File.Features.Music.GetMusic;
using TrashGrounds.File.Features.Music.UploadMusic;
using TrashGrounds.File.Infrastructure.Routing;

namespace TrashGrounds.File.Features.Music;

public class MusicEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/file/music")
            .WithTags("Музыка")
            .AddEndpoint<UploadMusicEndpoint>()
            .AddEndpoint<GetMusicEndpoint>();
    }
}