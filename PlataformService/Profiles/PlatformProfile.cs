using AutoMapper;
using PlataformService.Dtos;
using PlataformService.Models;

namespace PlataformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            //Source to target
            CreateMap<Platform, PlatformReadDto>();

            CreateMap<PlatformCreatDto, Platform>();
        }
    }
}
