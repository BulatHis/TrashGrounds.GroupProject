using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Models.Additional;

public class FullTrackInfo
{
    public TrackInfo? TrackInfo { get; set; }
    
    public UserInformation? UserInfo { get; set; }
    
    public Rate? Rate { get; set; }
}