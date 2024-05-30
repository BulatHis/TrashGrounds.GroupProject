using TrashGrounds.Rate.Models.Main.Abstractions;

namespace TrashGrounds.Rate.Models.Main;

public class TrackRate : BaseRate
{
    public Guid TrackId { get; set; }
}