using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public class MapperPole_Expertise : Profile
    {
        public MapperPole_Expertise() 
        {
            CreateMap<UpdatePole_ExpertiseDto, Pole_Expertise>();
        }
    }
}
