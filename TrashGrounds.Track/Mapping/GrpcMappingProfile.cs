using AutoMapper;
using TrackRateClient;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<TrackRate, Rate>()
            .ForMember(dest => dest.TrackId,
                opt =>
                    opt.MapFrom(src => Guid.Parse(src.Id)));
    }
}