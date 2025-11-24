using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public class MapperPlateforme : Profile
    {
        public MapperPlateforme() 
        {
            CreateMap<UpdatePlateformeDto, Plateforme>();
        }
    }
}
