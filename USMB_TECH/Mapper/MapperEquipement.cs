using USMB_TECH.DTO;
using USMB_TECH.Models;
using AutoMapper;
namespace USMB_TECH.Mapper
{
    public class MapperEquipement : Profile
    {
        public MapperEquipement()
        {
            CreateMap<EquipementAddDTO, Equipement>()
    .ForMember(dest => dest.PlateformeNavigation,
               opt => opt.MapFrom(src => new Plateforme { Nom_Plateforme = src.Nom_Plateforme }))
    .ForMember(dest => dest.Type_EquipementNavigation,
               opt => opt.MapFrom(src => new Type_Equipement { Nom_Type = src.Type_Equipement }))
    .ForMember(dest => dest.Id_Plateforme, opt => opt.Ignore())
    .ForMember(dest => dest.Id_Type_Equipement, opt => opt.Ignore());

        }
    }
}
