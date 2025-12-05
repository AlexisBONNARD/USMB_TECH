using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class AddLaboratoireDTO
    {
        public string Nom_Court { get; set; }

        public string Nom_Long { get; set; }

        public string Description { get; set; }
        public string? Rue_Adresse_Labo { get; set; }

        public string? Code_Postal_Adresse_Labo { get; set; }

        public string? Ville_Adresse_Labo { get; set; }

        public string? Pays_Adresse_Labo { get; set; }

        public string? Rue_Adresse_Campus { get; set; }

        public string? Code_Postal_Adresse_Campus { get; set; }

        public string? Ville_Adresse_Campus { get; set; }

        public string? Pays_Adresse_Campus { get; set; }

        public ICollection<string> Thematiques { get; set; }

        public ICollection<string> mot_Clefs { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
