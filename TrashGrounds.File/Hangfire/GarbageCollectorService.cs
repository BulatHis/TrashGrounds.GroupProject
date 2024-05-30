using Hangfire;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.File.Database.Postgres;
using TrashGrounds.File.gRPC.Services;
using TrashGrounds.File.Services.Interfaces;

namespace TrashGrounds.File.Hangfire;

public class GarbageCollectorService
{
    private readonly FileDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UsedFilesService _usedFilesService;

    public GarbageCollectorService(
        FileDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        UsedFilesService usedFilesService)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _usedFilesService = usedFilesService;
    }

    [Queue("gc")]
    public async Task DeleteUnusedTracks()
    {
        var usedIds = await _usedFilesService.GetUsedTrackIdsAsync();
        var currentDate = _dateTimeProvider.UtcNow;

        var notUsedTracks = await _context.MusicFiles
            .Where(music => (currentDate - music.UploadDate).TotalDays >= 1 && !usedIds.Contains(music.Id))
            .ToListAsync();

        _context.RemoveRange(notUsedTracks);
        await _context.SaveEntitiesAsync();

        RemoveUnusedFiles(notUsedTracks.Select(music => music.Route));
    }

    [Queue("gc")]
    public async Task DeleteUnusedImages()
    {
        var usedIds = await _usedFilesService.GetUsedImageIdsAsync();
        var currentDate = _dateTimeProvider.UtcNow;

        var notUsedImages = await _context.ImageFiles
            .Where(image => (currentDate - image.UploadDate).TotalDays >= 1 && !usedIds.Contains(image.Id))
            .ToListAsync();

        _context.RemoveRange(notUsedImages);
        await _context.SaveEntitiesAsync();

        RemoveUnusedFiles(notUsedImages.Select(image => image.Route));
    }

    private static void RemoveUnusedFiles(IEnumerable<string> routes)
    {
        foreach (var route in routes.Where(System.IO.File.Exists))
        {
            try
            {
                System.IO.File.Delete(route);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}