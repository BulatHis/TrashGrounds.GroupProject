namespace TrashGrounds.Track.Models.Additional;

public class TrackInfo
{
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public required int ListensCount { get; set; }
    
    public Guid? PictureId { get; set; }
    
    public required Guid UserId { get; set; }
}