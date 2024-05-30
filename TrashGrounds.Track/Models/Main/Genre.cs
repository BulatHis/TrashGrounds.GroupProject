using System.Text.Json.Serialization;
using TrashGrounds.Track.Models.Main.Abstractions;

namespace TrashGrounds.Track.Models.Main;

public class Genre : BaseEntity
{
    public required string Name { get; set; }
    
    [JsonIgnore]
    public List<MusicTrack> Tracks { get; set; }
}