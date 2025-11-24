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
            { Nom_Modele = src.Nom_Modele, 
                MarqueNavigation = new Marque
                {
                    Nom_Marque = src.Nom_Marque
                }
            }))
            .ForMember(dest => dest.Id_Modele, opt => opt.Ignore());

            CreateMap<EquipementDTO, Equipement>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.MapFrom(src => src.Id_Equipement))
                .ForMember(dest => dest.Id_Plateforme, opt => opt.MapFrom(src => src.Id_Plateforme))
                .ForMember(dest => dest.Id_Modele, opt => opt.MapFrom(src => src.Id_Modele))
                .ForMember(dest => dest.Id_Type_Equipement, opt => opt.MapFrom(src => src.Id_Type_Equipement))
                .ForMember(dest => dest.Nom_Equipement, opt => opt.MapFrom(src => src.Nom_Equipement))
                .ForMember(dest => dest.Num_Immobilisation, opt => opt.MapFrom(src => src.Num_Immobilisation))
                .ForMember(dest => dest.Date_Acquisition, opt => opt.MapFrom(src => src.Date_Acquisition))
                .ForMember(dest => dest.Prix_Achat, opt => opt.MapFrom(src => src.Prix_Achat))
                .ForMember(dest => dest.Prix_Revient, opt => opt.MapFrom(src => src.Prix_Revient))
                .ForMember(dest => dest.Description_Technique, opt => opt.MapFrom(src => src.Description_Technique))
                .ForMember(dest => dest.Disponibilite, opt => opt.MapFrom(src => src.Disponibilite))
                .ForMember(dest => dest.Autonomie, opt => opt.MapFrom(src => src.Autonomie))
                .ForMember(dest => dest.Utilisable_Chez_Le_Client, opt => opt.MapFrom(src => src.Utilisable_Chez_Le_Client))
                .ForMember(dest => dest.Actif, opt => opt.MapFrom(src => src.Actif))

                // ⚠️ Relations ignorées pour l’update (on travaille par ID)
                .ForMember(dest => dest.PlateformeNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Type_EquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModeleNavigation, opt => opt.Ignore());
        }

    }
}
