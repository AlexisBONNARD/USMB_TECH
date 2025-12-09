using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;
using System.Linq;
using System.Collections.Generic;

namespace USMB_TECH.Mapper
{
    public static class Pole_ExpertiseMapper
    {
        public static Pole_Expertise ToEntity(AddPole_ExpertiseDto dto)
        {
            var pole_expertise = new Pole_Expertise
            {
                Nom_Pole_Expertise = dto.Nom_Pole_Expertise,
                Description_Pole_Expertise = dto.Description_Pole_Expertise,
                Nom_Contenu = dto.Nom_Contenu,
                Url_Contenu = dto.Url_Contenu,
                Description_Contenu = dto.Description_Contenu,
                Actif = dto.Actif,
                Photos = dto.Photos.Select(p => new Photo
                {
                    Nom_Photo = p.Nom_Photo,
                    Url_Photo = p.Url_Photo
                }).ToList(),
                Exemple_Utilisations = dto.ExempleUtilisations.Select(e => new Exemple_Utilisation
                {
                    Nom_Utilisation = e.Nom_Utilisation,
                    Description_Utilisation = e.Description_Utilisation
                }).ToList(),
            };

            return pole_expertise;
        }

        public static Pole_Expertise ToEntity(UpdatePole_ExpertiseDto dto)
        {
            var pole_expertise = new Pole_Expertise
            {
                Id_Pole_Expertise = dto.Id_Pole_Expertise,
                Nom_Pole_Expertise = dto.Nom_Pole_Expertise,
                Description_Pole_Expertise = dto.Description_Pole_Expertise,
                Nom_Contenu = dto.Nom_Contenu,
                Url_Contenu = dto.Url_Contenu,
                Description_Contenu = dto.Description_Contenu,
                Actif = dto.Actif
            };

            // --- Collections ---
            pole_expertise.Photos = dto.Photos.Select(p => new Photo
            {
                Id_Photo = p.Id_Photo,
                Nom_Photo = p.Nom_Photo,
                Url_Photo = p.Url_Photo,
                Pole_ExpertiseNavigation = pole_expertise
            }).ToList();

            pole_expertise.Exemple_Utilisations = dto.ExempleUtilisations.Select(e => new Exemple_Utilisation
            {
                Id_Exemple_Utilisation = e.Id_Exemple_Utilisation,
                Nom_Utilisation = e.Nom_Utilisation,
                Description_Utilisation = e.Description_Utilisation,
                Pole_ExpertiseNavigation = pole_expertise
            }).ToList();

            pole_expertise.Presenters = dto.Presenters.Select(p => new Presenter
            {
                Id_Pole_Expertise = dto.Id_Pole_Expertise,
                Id_Prestation = p.Id_Prestation,
                Pole_ExpertiseNavigation = pole_expertise
            }).ToList();

            // --- Equipements (corrigé) ---
            pole_expertise.Equipements = dto.Equipements.Select(e => new Equipement
            {
                Id_Equipement = e.Id_Equipement,
                Id_Pole_Expertise = e.Id_Pole_Expertise,
                Id_Modele = e.Id_Modele,
                Id_Type_Equipement = e.Id_Type_Equipement,
                Nom_Equipement = e.Nom_Equipement,
                Num_Immobilisation = e.Num_Immobilisation,
                Date_Acquisition = e.Date_Acquisition,
                Prix_Achat = e.Prix_Achat,
                Prix_Revient = e.Prix_Revient,
                Description_Technique = e.Description_Technique,
                Disponibilite = e.Disponibilite,
                Autonomie = e.Autonomie,
                Utilisable_Chez_Le_Client = e.Utilisable_Chez_Le_Client,
                Actif = e.Actif,
                Pole_ExpertiseNavigation = pole_expertise
            }).ToList();

            pole_expertise.Specifiers = dto.MotsCles.Select(m => new Specifier
            {
                Id_Mot_Clef = m.Id_Mot_Clef,
                Pole_ExpertiseNavigation = pole_expertise
            }).ToList();

            return pole_expertise;
        }
    }

    //  Nouveau Profile AutoMapper pour PoleExpertisePreviewDTO
    public class PoleExpertiseProfile : Profile
    {
        public PoleExpertiseProfile()
        {
            CreateMap<Pole_Expertise, PoleExpertisePreviewDTO>()
                .ForMember(dest => dest.Id_Pole_Expertise, opt => opt.MapFrom(src => src.Id_Pole_Expertise))
                .ForMember(dest => dest.Nom_Pole_Expertise, opt => opt.MapFrom(src => src.Nom_Pole_Expertise))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description_Pole_Expertise))
                .ForMember(dest => dest.MotsCles, opt => opt.MapFrom(src =>
                    src.Specifiers != null
                        ? src.Specifiers
                            .Where(s => s.Mot_ClefNavigation != null)
                            .Select(s => s.Mot_ClefNavigation.Nom_Mot_Clef)
                            .Distinct()
                            .ToList()
                        : new List<string>()));
        }
    }
}
