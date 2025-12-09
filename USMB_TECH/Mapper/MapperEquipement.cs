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
            .ForMember(dest => dest.Id_Modele, opt => opt.Ignore());

            CreateMap<EquipementDTO, Equipement>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.MapFrom(src => src.Id_Equipement))
                .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.MapFrom(src => src.Id_Pole_Expertise))
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

                //  Relations ignorées pour l’update (on travaille par ID)
                .ForMember(dest => dest.Pole_ExpertiseNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Type_EquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModeleNavigation, opt => opt.Ignore());

            // Déjà existant
            CreateMap<Equipement, UpdateEquipementDto>()
                .ForMember(dest => dest.Nom_Pole_Expertise, opt => opt.MapFrom(src => src.Pole_ExpertiseNavigation.Nom_Pole_Expertise))
                .ForMember(dest => dest.Nom_Modele, opt => opt.MapFrom(src => src.ModeleNavigation.Nom_Modele))
                .ForMember(dest => dest.Nom_Marque, opt => opt.MapFrom(src => src.ModeleNavigation.MarqueNavigation.Nom_Marque))
                .ForMember(dest => dest.Type_Equipement, opt => opt.MapFrom(src => src.Type_EquipementNavigation.Nom_Type))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
                .ForMember(dest => dest.Exemple_Utilisations, opt => opt.MapFrom(src => src.Exemple_Utilisations))
                .ForMember(dest => dest.Fournirs, opt => opt.MapFrom(src => src.Fournirs));


            CreateMap<UpdateEquipementDto, Equipement>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.MapFrom(src => src.Id_Equipement))
                .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.MapFrom(src => src.Id_Pole_Expertise))
                .ForMember(dest => dest.Id_Modele, opt => opt.MapFrom(src => src.Id_Modele))
                .ForMember(dest => dest.Id_Type_Equipement, opt => opt.MapFrom(src => src.Id_Type_Equipement))
                .ForMember(dest => dest.Nom_Equipement, opt => opt.MapFrom(src => src.Nom_Equipement))
                .ForMember(dest => dest.Num_Immobilisation, opt => opt.MapFrom(src => src.Num_Immobilisation))
                .ForMember(dest => dest.Date_Acquisition, opt => opt.MapFrom(src => src.Date_Acquisition))
                .ForMember(dest => dest.Prix_Achat, opt => opt.MapFrom(src => (double)src.Prix_Achat))
                .ForMember(dest => dest.Prix_Revient, opt => opt.MapFrom(src => (double)src.Prix_Revient))
                .ForMember(dest => dest.Description_Technique, opt => opt.MapFrom(src => src.Description_Technique))
                .ForMember(dest => dest.Disponibilite, opt => opt.MapFrom(src => src.Disponibilite))
                .ForMember(dest => dest.Autonomie, opt => opt.MapFrom(src => src.Autonomie))
                .ForMember(dest => dest.Utilisable_Chez_Le_Client, opt => opt.MapFrom(src => src.Utilisable_Chez_Le_Client))
                .ForMember(dest => dest.Actif, opt => opt.MapFrom(src => src.Actif))

                // ⚡ ExempleUtilisations correctement mappés
                .ForMember(dest => dest.Exemple_Utilisations, opt => opt.MapFrom(src =>
                    src.Exemple_Utilisations.Select(eu => new Exemple_Utilisation
                    {
                        Id_Exemple_Utilisation = eu.Id_Exemple_Utilisation,
                        Nom_Utilisation = eu.Nom_Utilisation,
                        Description_Utilisation = eu.Description_Utilisation,
                        Id_Equipement = src.Id_Equipement
                    }).ToList()
                ))

                // ⚡ Photos corrigées pour respecter la contrainte
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src =>
                    src.Photos.Select(p => new Photo
                    {
                        Id_Photo = p.Id_Photo,
                        Nom_Photo = p.Nom_Photo,
                        Url_Photo = p.Url_Photo,
                        Id_Equipement = src.Id_Equipement,
                        Id_Pole_Expertise = null
                    }).ToList()
                ))


                .ForMember(dest => dest.Fournirs, opt => opt.Ignore());




            // --- Collections ---
            CreateMap<PhotoDto, Photo>();
            CreateMap<ExempleUtilisationDto, Exemple_Utilisation>();

            CreateMap<Equipement, EquipementPreviewDTO>()
                .ForMember(dest => dest.Id_Equipement, opt => opt.MapFrom(src => src.Id_Equipement))
                .ForMember(dest => dest.Nom_Equipement, opt => opt.MapFrom(src => src.Nom_Equipement))
                .ForMember(dest => dest.Description_Technique, opt => opt.MapFrom(src => src.Description_Technique))
                .ForMember(dest => dest.Nom_Pole_Expertise, opt => opt.MapFrom(src => src.Pole_ExpertiseNavigation != null
                    ? src.Pole_ExpertiseNavigation.Nom_Pole_Expertise
                    : null))
                .ForMember(dest => dest.Prix_Achat, opt => opt.MapFrom(src => src.Prix_Achat))
                .ForMember(dest => dest.Date_Acquisition, opt => opt.MapFrom(src => src.Date_Acquisition))
                .ForMember(dest => dest.Disponibilite, opt => opt.MapFrom(src => src.Disponibilite))
                .ForMember(dest => dest.MotsCles, opt => opt.MapFrom(src =>
                    src.Pole_ExpertiseNavigation != null && src.Pole_ExpertiseNavigation.Specifiers != null
                        ? src.Pole_ExpertiseNavigation.Specifiers
                            .Where(s => s.Mot_ClefNavigation != null)
                            .Select(s => s.Mot_ClefNavigation.Nom_Mot_Clef)
                            .Distinct()
                            .ToList()
                        : new List<string>()))
                .ForMember(dest => dest.Thematiques, opt => opt.MapFrom(src =>
                    src.Exposers != null
                        ? src.Exposers
                            .Where(t => t.ThematiqueNavigation != null)
                            .Select(t => t.ThematiqueNavigation.Nom_Thematique)
                            .Distinct()
                            .ToList()
                        : new List<string>()));
        }

    }
}
