using System;

namespace USMB_TECH.DTO
{
    public class PlateformeDTO
    {
        public int Id_Plateforme { get; set; }
        public string Nom_Plateforme { get; set; }
        public string Description_Plateforme { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PlateformeDTO DTO &&
                   Id_Plateforme == DTO.Id_Plateforme &&
                   Nom_Plateforme == DTO.Nom_Plateforme &&
                   Description_Plateforme == DTO.Description_Plateforme &&
                   Nom_Contenu == DTO.Nom_Contenu &&
                   Url_Contenu == DTO.Url_Contenu &&
                   Description_Contenu == DTO.Description_Contenu;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Plateforme);
            hash.Add(Nom_Plateforme);
            hash.Add(Description_Plateforme);
            hash.Add(Nom_Contenu);
            hash.Add(Url_Contenu);
            hash.Add(Description_Contenu);
            return hash.ToHashCode();
        }
    }
}
