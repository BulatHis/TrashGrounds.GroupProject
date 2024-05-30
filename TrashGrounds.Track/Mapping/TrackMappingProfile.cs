using AutoMapper;
using TrashGrounds.Track.Features.Track.AddTrack;
using TrashGrounds.Track.Models.Main;
using TrashGrounds.Track.Services.Interfaces;

namespace TrashGrounds.Track.Mapping;

public class TrackMappingProfile : Profile
{
    public TrackMappingProfile(){}
    
    public TrackMappingProfile(IDateTimeProvider dateTimeProvider)
    {
        CreateMap<AddTrackCommand, MusicTrack>()
            .ForMember(dest => dest.ListensCount,
                opt =>
                    opt.MapFrom(src => 0))
            .ForMember(dest => dest.Genres,
                opt =>
                    opt.MapFrom(src => new List<Genre>()));
    }
}