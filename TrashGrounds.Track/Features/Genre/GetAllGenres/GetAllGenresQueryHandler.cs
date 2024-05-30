using Microsoft.EntityFrameworkCore;
using TrashGrounds.Track.Database.Postgres;
using TrashGrounds.Track.Infrastructure.Mediator.Query;

namespace TrashGrounds.Track.Features.Genre.GetAllGenres;

public class GetAllGenresQueryHandler : IQueryHandler<GetAllGenresQuery, GetAllGenresResponse>
{
    private readonly TrackDbContext _context;

    public GetAllGenresQueryHandler(TrackDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllGenresResponse> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return new GetAllGenresResponse(
            await _context.Genres.AsNoTracking().ToListAsync(cancellationToken));
    }
}