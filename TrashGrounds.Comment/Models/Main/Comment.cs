using TrashGrounds.Comment.Models.Main.Abstractions;

namespace TrashGrounds.Comment.Models.Main;

public class Comment : BaseEntity
{
    public required string Message { get; set; }
    
    public DateTime SendAt { get; set; }
    
    public DateTime? EditedAt { get; set; }
    
    public Guid? ReplyTo { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid TrackId { get; set; }
}