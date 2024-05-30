using TrashGrounds.Post.Models.Main.Abstractions;

namespace TrashGrounds.Post.Models.Main;

public class Post : BaseEntity
{
    public required string Text { get; set; }
    
    public DateTime UploadDate { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid? AssetId { get; set; }
    
    public bool IsHidden { get; set; }
}