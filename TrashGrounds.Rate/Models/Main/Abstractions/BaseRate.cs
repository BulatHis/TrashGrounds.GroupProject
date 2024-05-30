namespace TrashGrounds.Rate.Models.Main.Abstractions;

public class BaseRate : BaseEntity
{
    public Guid UserId { get; set; }
    
    public int Rate { get; set; }
    
    public DateTime DateTime { get; set; }
}