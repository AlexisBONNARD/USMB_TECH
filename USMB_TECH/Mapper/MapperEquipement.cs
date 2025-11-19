using USMB_TECH.DTO;
using USMB_TECH.Models;
using AutoMapper;
namespace USMB_TECH.Mapper
{
    public class MapperEquipement : Profile
    {
        public MapperEquipement()
        {
            CreateMap<EquipementAddDTO, Equipement>();
        }
    }
}
