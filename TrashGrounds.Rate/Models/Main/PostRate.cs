using TrashGrounds.Rate.Models.Main.Abstractions;

namespace TrashGrounds.Rate.Models.Main;

public class PostRate : BaseRate
{
    public Guid PostId { get; set; }
}