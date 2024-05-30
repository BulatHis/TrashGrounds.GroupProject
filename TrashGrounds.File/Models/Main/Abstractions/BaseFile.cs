namespace TrashGrounds.File.Models.Main.Abstractions;

public abstract class BaseFile : BaseEntity
{
    public required string Route { get; set; }
    
    public DateTime UploadDate { get; set; }
    
    public Guid UserId { get; set; }
}