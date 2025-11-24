using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;
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
                .ForMember(dest => dest.Id_Type_Equipement, opt => opt.Ignore())
                .ForMember(dest => dest.ModeleNavigation,
                    opt => opt.MapFrom(src => new Modele
                    {
                        Nom_Modele = src.Nom_Modele,
                        MarqueNavigation = new Marque
                        {
                            Nom_Marque = src.Nom_Marque
                        }
                    }))
                .ForMember(dest => dest.Id_Modele, opt => opt.Ignore())
                .ForMember(dest => dest.Photos,
                    opt => opt.MapFrom(src => src.Photos.Select(p => new Photo
                    {
                        Nom_Photo = p.Nom_Photo,
                        Url_Photo = p.Url_Photo
                    }).ToList()
                ));
        }
    }
}
