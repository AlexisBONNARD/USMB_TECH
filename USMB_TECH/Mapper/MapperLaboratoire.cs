using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public class MapperLaboratoire : Profile
    {
        public MapperLaboratoire()
        {
            CreateMap<AddLaboratoireDTO, Laboratoire>()
            .ForMember(dest => dest.Adresse_campusNavigation,
            opt => opt.MapFrom(src => new Adresse
            {
                Rue_Adresse = src.Rue_Adresse_Campus,
                Code_Postal_Adresse = src.Code_Postal_Adresse_Campus,
                Ville_Adresse = src.Ville_Adresse_Campus,
                Pays_Adresse = src.Pays_Adresse_Campus
            }))
            .ForMember(dest => dest.Adresse_laboNavigation,
            opt => opt.MapFrom(src => new Adresse
            {
                Rue_Adresse = src.Rue_Adresse_Labo,
                Code_Postal_Adresse = src.Code_Postal_Adresse_Labo,
                Ville_Adresse = src.Ville_Adresse_Labo,
                Pays_Adresse = src.Pays_Adresse_Labo
            }))
            .ForMember(dest => dest.Designers, opt => opt.MapFrom(src => src.mot_Clefs.Select(mc => new Designer
            {
                Mot_ClefNavigation = new Mot_Clef { Nom_Mot_Clef = mc },
                Nom_Court = src.Nom_Court
            }).ToList())
            )
            .ForMember(dest => dest.Id_Adresse_Labo, opt => opt.Ignore())
            .ForMember(dest => dest.Id_Adresse_Campus, opt => opt.Ignore());
        }
    }
}
