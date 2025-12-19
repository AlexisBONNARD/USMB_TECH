using System;

namespace USMB_TECH.DTO
{
    public class Pole_ExpertiseDTO
    {
        public int Id_Pole_Expertise { get; set; }
        public string Nom_Pole_Expertise { get; set; }
        public string Description_Pole_Expertise { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Pole_ExpertiseDTO DTO &&
                   Id_Pole_Expertise == DTO.Id_Pole_Expertise &&
                   Nom_Pole_Expertise == DTO.Nom_Pole_Expertise &&
                   Description_Pole_Expertise == DTO.Description_Pole_Expertise &&
                   Nom_Contenu == DTO.Nom_Contenu &&
                   Url_Contenu == DTO.Url_Contenu &&
                   Description_Contenu == DTO.Description_Contenu;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id_Pole_Expertise);
            hash.Add(Nom_Pole_Expertise);
            hash.Add(Description_Pole_Expertise);
            hash.Add(Nom_Contenu);
            hash.Add(Url_Contenu);
            hash.Add(Description_Contenu);
            return hash.ToHashCode();
        }
    }
}
