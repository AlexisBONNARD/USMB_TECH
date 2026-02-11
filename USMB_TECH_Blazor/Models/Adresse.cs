using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Adresse
    {
        public int Id_Adresse { get; set; }

        [MaxLength(200, ErrorMessage = "La Rue ne doit pas dépasser 200 caractères")]
        public string? Rue_Adresse { get; set; }

        [MaxLength(200, ErrorMessage = "Le complément Rue ne doit pas dépasser 200 caractères")]
        public string? Complement_Rue_Adresse { get; set; }

        [MaxLength(11, ErrorMessage = "Le Code Postal ne doit pas dépasser 11 caractères")]
        [RegularExpression(@"^(2[AB]|[1-9][0-9]{4})$", ErrorMessage = "Le Code Postal doit étre cinstituer de (2A, 2B ou chiffres).")]
        public string? Code_Postal_Adresse { get; set; }

        [MaxLength(100, ErrorMessage = "La Ville ne doit pas dépasser 100 caractères")]
        public string? Ville_Adresse { get; set; }

        [MaxLength(50, ErrorMessage = "Le pays ne doit pas dépasser 50 caractères")]
        public string? Pays_Adresse { get; set; }

        public virtual ICollection<Laboratoire> Laboratoires_campus { get; set; } = new List<Laboratoire>();

        public virtual ICollection<Laboratoire> Laboratoires_labo { get; set; } = new List<Laboratoire>();
    }
}
