using FileClient;
using TrashGrounds.User.Infrastructure.Exceptions;

namespace TrashGrounds.User.gRPC.Services;

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
}