using asp.net_core_web_api_reference_project.DTO;
using asp.net_core_web_api_reference_project.Models;
using AutoMapper;

namespace asp.net_core_web_api_reference_project.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();  // we don't need .ReverseMap() but it's ok to have it
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();  // TSource , TDestination
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk,  WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
