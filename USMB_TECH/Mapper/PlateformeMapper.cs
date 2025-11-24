using AutoMapper;
using USMB_TECH.DTO;
using USMB_TECH.Models;

namespace USMB_TECH.Mapper
{
    public static class PlateformeMapper
    {
        public static Plateforme ToEntity(AddPlateformeDto dto)
        {
            var plateforme = new Plateforme
            {
                Nom_Plateforme = dto.Nom_Plateforme,
                Description_Plateforme = dto.Description_Plateforme,
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

            return plateforme;
        }

        public static Plateforme ToEntity(UpdatePlateformeDto dto)
        {
            var plateforme = new Plateforme
            {
                Id_Plateforme = dto.Id_Plateforme,
                Nom_Plateforme = dto.Nom_Plateforme,
                Description_Plateforme = dto.Description_Plateforme,
                Nom_Contenu = dto.Nom_Contenu,
                Url_Contenu = dto.Url_Contenu,
                Description_Contenu = dto.Description_Contenu,
                Actif = dto.Actif
            };

            // --- Collections ---
            plateforme.Photos = dto.Photos.Select(p => new Photo
            {
                Id_Photo = p.Id_Photo,
                Nom_Photo = p.Nom_Photo,
                Url_Photo = p.Url_Photo,
                PlateformeNavigation = plateforme
            }).ToList();

            plateforme.Exemple_Utilisations = dto.ExempleUtilisations.Select(e => new Exemple_Utilisation
            {
                Id_Exemple_Utilisation = e.Id_Exemple_Utilisation,
                Nom_Utilisation = e.Nom_Utilisation,
                Description_Utilisation = e.Description_Utilisation,
                PlateformeNavigation = plateforme
            }).ToList();

            plateforme.Presenters = dto.Presenters.Select(p => new Presenter
            {
                Id_Plateforme = dto.Id_Plateforme,
                Id_Prestation = p.Id_Prestation,
                PlateformeNavigation = plateforme
            }).ToList();

            // --- Equipements (corrigé) ---
            plateforme.Equipements = dto.Equipements.Select(e => new Equipement
            {
                Id_Equipement = e.Id_Equipement,
                Id_Plateforme = e.Id_Plateforme,
                Id_Modele = e.Id_Modele,
                Id_Type_Equipement = e.Id_Type_Equipement,
                Nom_Equipement = e.Nom_Equipement,
                Num_Immobilisation = e.Num_Immobilisation,   //obligatoire
                Date_Acquisition = e.Date_Acquisition,
                Prix_Achat = e.Prix_Achat,
                Prix_Revient = e.Prix_Revient,
                Description_Technique = e.Description_Technique,
                Disponibilite = e.Disponibilite,
                Autonomie = e.Autonomie,
                Utilisable_Chez_Le_Client = e.Utilisable_Chez_Le_Client,
                Actif = e.Actif,
                PlateformeNavigation = plateforme
            }).ToList();

            plateforme.Specifiers = dto.MotsCles.Select(m => new Specifier
            {
                Id_Mot_Clef = m.Id_Mot_Clef,
                PlateformeNavigation = plateforme
            }).ToList();

            plateforme.Exposers = dto.Thematiques.Select(t => new Exposer
            {
                Id_Thematique = t.Id_Thematique,
                PlateformeNavigation = plateforme
            }).ToList();

            return plateforme;
        }
    }
}
