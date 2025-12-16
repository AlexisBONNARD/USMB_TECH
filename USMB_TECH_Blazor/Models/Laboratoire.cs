using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Laboratoire
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

        [Required(ErrorMessage = "Le pays du campus est obligatoire")]
        public string? Pays_Adresse_Campus { get; set; }

        public ICollection<string> Pole_Expertises { get; set; } = new List<string>();

        public Adresse? Adresse_campusNavigation { get; set; } = null!;
        public Adresse? Adresse_laboNavigation { get; set; } = null!;

        public ICollection<string> mot_Clefs { get; set; } = new List<string>();

        public ICollection<string> Thematiques { get; set; } = new List<string>();

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public  ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();

        public ICollection<Contact_USMB> Contacts { get; set; } = new List<Contact_USMB>();

        public ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();
    }
}
