using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Models.Additional;

public class FullTrack
{
    public MusicTrack? Track { get; set; }
    
    public UserInformation? UserInfo { get; set; }
    
    public Rate? Rate { get; set; }
}