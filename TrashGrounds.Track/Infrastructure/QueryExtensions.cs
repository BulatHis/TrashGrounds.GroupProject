using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Infrastructure.Exceptions;
using TrashGrounds.Track.Models.Additional;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Infrastructure;

public static class QueryExtensions
{
    public static async Task<T> FirstOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        return await queryable.FirstOrDefaultAsync(cancellationToken) 
               ?? throw new NotFoundException<T>();
    }
    
    public static async Task<T> FirstOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken) 
               ?? throw new NotFoundException<T>();
    }
    
    public static async Task<bool> AnyOrNotFoundAsync<T>(
        this IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await queryable.AnyAsync(predicate, cancellationToken) 
            ? true 
            : throw new NotFoundException<T>();
    }

    public static async Task<List<TrackInfo>> ToTracksInfo(
        this IQueryable<MusicTrack> queryable,
        int take, int skip = 0)
    {
        return await queryable
            .Skip(skip)
            .Take(take)
            .Select(track => new TrackInfo
            {
                Id = track.Id,
                Title = track.Title,
                ListensCount = track.ListensCount,
                PictureId = track.PictureId,
                UserId = track.UserId
            })
            .ToListAsync();
    }

    public static IEnumerable<FullTrackInfo> ToFullTrackInfo(this IEnumerable<TrackInfo> tracks,
        IEnumerable<UserInformation>? users, IEnumerable<Rate>? rates)
    {
        return tracks.Select(track => new FullTrackInfo
        {
            TrackInfo = track,
            UserInfo = users?.FirstOrDefault(user => user.Id == track.UserId),
            Rate = rates?.FirstOrDefault(rate => rate.TrackId == track.Id)
        });
    }
}