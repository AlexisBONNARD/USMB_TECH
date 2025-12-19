using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class LaboratoireUpdate
    {
        [Required(ErrorMessage = "Le laboratoire a besoin d'un raccourci pour son nom")]
        public string Nom_Court { get; set; }

        public int Id_Adresse_Campus { get; set; }
        public int Id_Adresse_Labo { get; set; }

        [Required(ErrorMessage = "Le laboratoire a besoin d'un nom long")]
        public string Nom_Long { get; set; }

        public string Description { get; set; }

        // Relations N–N : uniquement des IDs
        public List<int> Pole_Expertises { get; set; } = new();
        public List<int> mot_Clefs { get; set; } = new();
        public List<int> Thematiques { get; set; } = new();

        // Navigation pour les adresses
        public Adresse? Adresse_campusNavigation { get; set; }
        public Adresse? Adresse_laboNavigation { get; set; }
    }

}
