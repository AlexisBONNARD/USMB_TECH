
using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public class MapperEquipement : Profile
    {
        public MapperEquipement()
        {
            CreateMap<AddEquipementDTO, Equipement>()
            .ForMember(dest => dest.Pole_ExpertiseNavigation,
               opt => opt.MapFrom(src => new Pole_Expertise { Nom_Pole_Expertise = src.Nom_Pole_Expertise }))
            .ForMember(dest => dest.Type_EquipementNavigation,
               opt => opt.MapFrom(src => new Type_Equipement { Nom_Type = src.Type_Equipement }))
            .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.Ignore())
            .ForMember(dest => dest.Url_Modele_3D, opt => opt.Ignore())
            .ForMember(dest => dest.Consommers, opt => opt.Ignore())
            .ForMember(dest => dest.Referencers, opt => opt.Ignore())
            .ForMember(dest => dest.Prise_Contacts, opt => opt.Ignore())
            .ForMember(dest => dest.Disponibilite, opt => opt.Ignore())
            .ForMember(dest => dest.Fournirs, opt => opt.Ignore())
            .ForMember(dest => dest.Exposers, opt => opt.Ignore())
            .ForMember(dest => dest.Id_Equipement, opt => opt.Ignore())
            .ForMember(dest => dest.Exemple_Utilisations,
                opt => opt.MapFrom(src => new List<Exemple_Utilisation>
                {
                    new Exemple_Utilisation
                    {
                        Nom_Utilisation = src.Nom_Exemple,
                        Description_Utilisation = src.Description_Exemple
                    }
                }))
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
            .ForMember(dest => dest.Qualifiers, opt => opt.MapFrom(src =>
                src.MotsCles != null
                    ? src.MotsCles
                        .Where(m => !string.IsNullOrWhiteSpace(m))
                        .Select(m => new Qualifier
                        {
                            Mot_ClefNavigation = new Mot_Clef
                            {
                                Nom_Mot_Clef = m.Trim()
                            }
                        })
                        .ToList()
                    : new List<Qualifier>()
            ))
            .ForMember(dest => dest.Posseders,
                opt => opt.MapFrom(src => src.Fonctionnalites
                    .Select(f => new Posseder
                    {
                        FonctionnaliteNavigation = new Fonctionnalite
                        {
                            Nom_Fonctionnalite = f.Nom_Fonctionnalite,
                            Description = f.Description
                        }
                    }).ToList()
                )
            );

            CreateMap<EquipementDTO, Equipement>()
                .ForMember(dest => dest.Url_Modele_3D, opt => opt.Ignore())
                .ForMember(dest => dest.Consommers, opt => opt.Ignore())
                .ForMember(dest => dest.Posseders, opt => opt.Ignore())
                .ForMember(dest => dest.Exemple_Utilisations, opt => opt.Ignore())
                .ForMember(dest => dest.Referencers, opt => opt.Ignore())
                .ForMember(dest => dest.Prise_Contacts, opt => opt.Ignore())
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.Fournirs, opt => opt.Ignore())
                .ForMember(dest => dest.Qualifiers, opt => opt.Ignore())
                .ForMember(dest => dest.Exposers, opt => opt.Ignore())
                .ForMember(dest => dest.Pole_ExpertiseNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModeleNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Type_EquipementNavigation, opt => opt.Ignore());

            CreateMap<UpdateEquipementDto, Equipement>()
                .ForMember(dest => dest.Url_Modele_3D, opt => opt.Ignore())
                .ForMember(dest => dest.Pole_ExpertiseNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModeleNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Type_EquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Consommers, opt => opt.Ignore())
                .ForMember(dest => dest.Posseders, opt => opt.Ignore())
                .ForMember(dest => dest.Referencers, opt => opt.Ignore())
                .ForMember(dest => dest.Prise_Contacts, opt => opt.Ignore())
                .ForMember(dest => dest.Qualifiers, opt => opt.Ignore())
                .ForMember(dest => dest.Exposers, opt => opt.Ignore());

            CreateMap<PhotoDto, Photo>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Domaine_Excellence, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Prestation, opt => opt.Ignore())
                .ForMember(dest => dest.Nom_Court, opt => opt.Ignore())
                .ForMember(dest => dest.EquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Pole_ExpertiseNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Domaine_ExcellenceNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.PrestationNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.LaboratoireNavigation, opt => opt.Ignore());

            CreateMap<ExempleUtilisationDto, Exemple_Utilisation>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.Ignore())
                .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.Ignore())
                .ForMember(dest => dest.EquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Pole_ExpertiseNavigation, opt => opt.Ignore());

            CreateMap<Equipement, EquipementPreviewDTO>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.MapFrom(src => src.Id_Equipement))
                .ForMember(dest => dest.Nom_Equipement, opt => opt.MapFrom(src => src.Nom_Equipement))
                .ForMember(dest => dest.Description_Technique, opt => opt.MapFrom(src => src.Description_Technique))
                .ForMember(dest => dest.Nom_Pole_Expertise, opt => opt.MapFrom(src => src.Pole_ExpertiseNavigation != null? src.Pole_ExpertiseNavigation.Nom_Pole_Expertise : null))
                .ForMember(dest => dest.Prix_Achat, opt => opt.MapFrom(src => src.Prix_Achat))
                .ForMember(dest => dest.Date_Acquisition, opt => opt.MapFrom(src => src.Date_Acquisition))
                .ForMember(dest => dest.Disponibilite, opt => opt.MapFrom(src => src.Disponibilite))
                .ForMember(dest => dest.MotsCles, opt => opt.MapFrom(src => src.Pole_ExpertiseNavigation != null && src.Pole_ExpertiseNavigation.Specifiers != null? src.Pole_ExpertiseNavigation.Specifiers
                    .Where(s => s.Mot_ClefNavigation != null)
                        .Select(s => s.Mot_ClefNavigation.Nom_Mot_Clef)
                            .Distinct()
                                .ToList(): new List<string>()))
                .ForMember(dest => dest.Thematiques, opt => opt.MapFrom(src =>src.Exposers != null? src.Exposers
                    .Where(t => t.ThematiqueNavigation != null)
                        .Select(t => t.ThematiqueNavigation.Nom_Thematique)
                            .Distinct()
                            .ToList(): new List<string>()));
            CreateMap<Type_UtilisationDTO, Type_Utilisation>().ReverseMap();
            CreateMap<Type_ClientDTO, Type_Client>().ReverseMap();
            CreateMap<AddType_UtilisationDTO, Type_Utilisation>();
            CreateMap<AddType_ClientDTO, Type_Client>();
        }
    }
}
