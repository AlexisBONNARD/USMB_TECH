using System;
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class LaboratoireDTO
    {
        public string Nom_Court { get; set; }
        public int Id_Adresse_Campus { get; set; }
        public int Id_Adresse_Labo { get; set; }
        public string Nom_Long { get; set; }
        public string Description { get; set; }
        public virtual Adresse? Adresse_campusNavigation { get; set; } = null!;
        public virtual Adresse? Adresse_laboNavigation { get; set; } = null!;
        
        public override bool Equals(object? obj)
        {
            return obj is LaboratoireDTO DTO &&
                   Nom_Court == DTO.Nom_Court &&
                   Id_Adresse_Campus == DTO.Id_Adresse_Campus &&
                   Id_Adresse_Labo == DTO.Id_Adresse_Labo &&
                   Nom_Long == DTO.Nom_Long &&
                   Description == DTO.Description &&
                   Adresse_campusNavigation == DTO.Adresse_campusNavigation &&
                   Adresse_laboNavigation == DTO.Adresse_laboNavigation;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Nom_Court);
            hash.Add(Id_Adresse_Campus);
            hash.Add(Id_Adresse_Labo);
            hash.Add(Nom_Long);
            hash.Add(Description);
            hash.Add(Adresse_campusNavigation);
            hash.Add(Adresse_laboNavigation);
            return hash.ToHashCode();
        }
    }
}
