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

        [Required(ErrorMessage = "La rue du laboratoire est obligatoire")]
        public string? Rue_Adresse_Labo { get; set; }

        [Required(ErrorMessage = "Le code postal du laboratoire est obligatoire")]
        [RegularExpression(@"^[0-9A-Za-z\- ]{1,11}$")]
        public string? Code_Postal_Adresse_Labo { get; set; }

        [Required(ErrorMessage = "La ville du laboratoire est obligatoire")]
        public string? Ville_Adresse_Labo { get; set; }

        [Required(ErrorMessage = "Le pays du laboratoire est obligatoire")]
        public string? Pays_Adresse_Labo { get; set; }

        [Required(ErrorMessage = "La rue du campus est obligatoire")]
        public string? Rue_Adresse_Campus { get; set; }

        [Required(ErrorMessage = "Le code postal du campus est obligatoire")]
        [RegularExpression(@"^[0-9A-Za-z\- ]{1,11}$")]
        public string? Code_Postal_Adresse_Campus { get; set; }

        [Required(ErrorMessage = "La ville du campus est obligatoire")]
        public string? Ville_Adresse_Campus { get; set; }
        public string? Pays_Adresse_Campus { get; set; }

        public ICollection<string> Pole_Expertises { get; set; } = new List<string>();

        public Adresse? Adresse_campusNavigation { get; set; } = null!;
        public Adresse? Adresse_laboNavigation { get; set; } = null!;

        public ICollection<string> mot_Clefs { get; set; } = new List<string>();

        public ICollection<string> Thematiques { get; set; } = new List<string>();
        

        public ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}
