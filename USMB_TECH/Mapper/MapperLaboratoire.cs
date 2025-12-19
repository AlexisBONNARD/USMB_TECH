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
            }).ToList()))
            .ForMember(dest => dest.Est_Liers, opt => opt.MapFrom(src => src.Thematiques.Select(t => new Est_Lier 
            {
                ThematiqueNavigation = new Thematique { Nom_Thematique = t },
                Nom_Court = src.Nom_Court,
            }).ToList()))
            .ForMember(dest => dest.Gerers, opt => opt.MapFrom(src => src.Pole_Expertises.Select(pe => new Gerer
            {
                Nom_Court = src.Nom_Court,
                Pole_ExpertiseNavigation = new Pole_Expertise { Nom_Pole_Expertise =  pe}
            }).ToList()))
            .ForMember(dest => dest.Id_Adresse_Labo, opt => opt.Ignore())
            .ForMember(dest => dest.Id_Adresse_Campus, opt => opt.Ignore());

            CreateMap<Laboratoire, LaboratoirePreviewDTO>()
            .ForMember(dest => dest.Nom_Court, opt => opt.MapFrom(src => src.Nom_Court))
            .ForMember(dest => dest.Nom_Long, opt => opt.MapFrom(src => src.Nom_Long))
            .ForMember(dest => dest.Ville, opt => opt.MapFrom(src => src.Adresse_laboNavigation != null ? src.Adresse_laboNavigation.Ville_Adresse : null))
            .ForMember(dest => dest.Pays, opt => opt.MapFrom(src => src.Adresse_laboNavigation != null ? src.Adresse_laboNavigation.Pays_Adresse : null))
            .ForMember(dest => dest.MotsCles, opt => opt.MapFrom(src =>
                src.Designers != null
                    ? src.Designers
                        .Where(d => d.Mot_ClefNavigation != null)
                        .Select(d => d.Mot_ClefNavigation.Nom_Mot_Clef)
                        .Distinct()
                        .ToList()
                    : new List<string>()))
            .ForMember(dest => dest.Thematiques, opt => opt.MapFrom(src =>
                src.Est_Liers != null
                    ? src.Est_Liers
                        .Where(t => t.ThematiqueNavigation != null)
                        .Select(t => t.ThematiqueNavigation.Nom_Thematique)
                        .Distinct()
                        .ToList()
                    : new List<string>()));

            CreateMap<LaboratoireUpdateDTO, Laboratoire>()
                .ForMember(dest => dest.Nom_Court, opt => opt.MapFrom(src => src.Nom_Court))
                .ForMember(dest => dest.Nom_Long, opt => opt.MapFrom(src => src.Nom_Long))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))

                .ForMember(dest => dest.Id_Adresse_Campus, opt => opt.MapFrom(src => src.Id_Adresse_Campus))
                .ForMember(dest => dest.Id_Adresse_Labo, opt => opt.MapFrom(src => src.Id_Adresse_Labo))

                // Relations N–N
                .ForMember(dest => dest.Gerers, opt => opt.MapFrom(src =>
                    src.Pole_Expertises.Select(id => new Gerer { Id_Pole_Expertise = id }).ToList()))

                .ForMember(dest => dest.Designers, opt => opt.MapFrom(src =>
                    src.mot_Clefs.Select(id => new Designer { Id_Mot_Clef = id }).ToList()))

                .ForMember(dest => dest.Est_Liers, opt => opt.MapFrom(src =>
                    src.Thematiques.Select(id => new Est_Lier { Id_Thematique = id }).ToList()))

                .ForMember(dest => dest.Prestations, opt => opt.Ignore());


        }
    }
}
