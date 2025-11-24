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
    }
}


    



