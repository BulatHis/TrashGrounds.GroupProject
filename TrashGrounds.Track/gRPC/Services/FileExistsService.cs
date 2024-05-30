using FileClient;
using TrashGrounds.Track.Infrastructure.Exceptions;

namespace TrashGrounds.Track.gRPC.Services;

public class FileExistsService
{
    private readonly FileService.FileServiceClient _fileServiceClient;

    public FileExistsService(FileService.FileServiceClient fileServiceClient)
    {
        _fileServiceClient = fileServiceClient;
    }

    public async Task<bool> IsImageExistsAsync(Guid imageId)
    {
        try
        {
            var response = await _fileServiceClient.CheckImageExistsAsync(new ImageRequest
            {
                ImageId = imageId.ToString()
            });
            return response.Exists;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DomainException("Cant connect to Files");
        }
    }

    public async Task<bool> IsTrackExistsAsync(Guid trackId)
    {
        try
        {
            var response = await _fileServiceClient.CheckTrackExistsAsync(new TrackRequest
            {
                TrackId = trackId.ToString()
            });
            return response.Exists;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new DomainException("Cant connect to Files");
        }
    }
}