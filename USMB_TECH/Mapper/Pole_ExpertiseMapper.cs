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
                Actif = dto.Actif,
                Id_Domaine_Excellence = dto.Id_Domaine_Excellence,
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
                Specifiers = dto.Specifiers.Select(s => new Specifier
                {
                    Id_Mot_Clef = s.Id_Mot_Clef,
                    Id_Pole_Expertise = s.Id_Pole_Expertise
                }).ToList()
            };

            return pole_expertise;
        }

        public static Pole_Expertise ToEntity(UpdatePole_ExpertiseDto dto)
        {
            var pole_expertise = new Pole_Expertise
            {
                //  Identité
                Id_Pole_Expertise = dto.Id_Pole_Expertise,

                //  Domaine d'excellence (OBLIGATOIRE en update)
                Id_Domaine_Excellence = dto.Id_Domaine_Excellence,

                // --- Propriétés simples ---
                Nom_Pole_Expertise = dto.Nom_Pole_Expertise?.Trim(),
                Description_Pole_Expertise = dto.Description_Pole_Expertise?.Trim(),
                Actif = dto.Actif
            };

            // -------------------------
            // Photos
            // -------------------------
            pole_expertise.Photos = dto.Photos?.Select(p => new Photo
            {
                Id_Photo = p.Id_Photo,
                Nom_Photo = p.Nom_Photo?.Trim(),
                Url_Photo = p.Url_Photo?.Trim()
            }).ToList() ?? new List<Photo>();


            // -------------------------
            // Exemples d'utilisation
            // -------------------------
            pole_expertise.Exemple_Utilisations = dto.ExempleUtilisations?.Select(e => new Exemple_Utilisation
            {
                Id_Exemple_Utilisation = e.Id_Exemple_Utilisation,
                Nom_Utilisation = e.Nom_Utilisation?.Trim(),
                Description_Utilisation = e.Description_Utilisation?.Trim()
            }).ToList() ?? new List<Exemple_Utilisation>();


            // -------------------------
            // Presenters (relation N-N)
            // -------------------------
            pole_expertise.Presenters = dto.Presenters?.Select(p => new Presenter
            {
                Id_Pole_Expertise = dto.Id_Pole_Expertise,
                Id_Prestation = p.Id_Prestation
            }).ToList() ?? new List<Presenter>();


            // -------------------------
            // Equipements (incrémental)
            // -------------------------
            pole_expertise.Equipements = dto.Equipements?.Select(e => new Equipement
            {
                Id_Equipement = e.Id_Equipement,

                //  FK volontairement NON touchée ici
                // Id_Pole_Expertise gérée côté manager

                Id_Modele = e.Id_Modele,
                Id_Type_Equipement = e.Id_Type_Equipement,
                Nom_Equipement = e.Nom_Equipement?.Trim(),
                Num_Immobilisation = e.Num_Immobilisation?.Trim(),
                Date_Acquisition = e.Date_Acquisition,
                Prix_Achat = e.Prix_Achat,
                Prix_Revient = e.Prix_Revient,
                Description_Technique = e.Description_Technique?.Trim(),
                Disponibilite = e.Disponibilite,
                Autonomie = e.Autonomie,
                Utilisable_Chez_Le_Client = e.Utilisable_Chez_Le_Client,
                Actif = e.Actif
            }).ToList() ?? new List<Equipement>();


            // -------------------------
            // Mots-clés (Specifier)
            // -------------------------
            pole_expertise.Specifiers = dto.MotsCles?.Select(m => new Specifier
            {
                Id_Mot_Clef = m.Id_Mot_Clef
            }).ToList() ?? new List<Specifier>();


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
