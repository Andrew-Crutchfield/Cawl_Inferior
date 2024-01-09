using AutoMapper;
using CawlInferior.SpaceMarines.Dto;

namespace CawlInferior.SpaceMarines
{
    public class SpaceMarineApplicationAutoMapperProfile : Profile
    {
        public SpaceMarineApplicationAutoMapperProfile()
        {
            CreateMap<SpaceMarineDataSheet, SpaceMarineDto>();
        }
    }
}
