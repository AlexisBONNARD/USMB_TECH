using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Adresse
    {
        public int Id_Adresse { get; set; }

        public string? Rue_Adresse { get; set; }

        public string? Complement_Rue_Adresse { get; set; }

        public string? Code_Postal_Adresse { get; set; }

        public string? Ville_Adresse { get; set; }

        public string? Pays_Adresse { get; set; }

        public virtual ICollection<Laboratoire> Laboratoires_campus { get; set; } = new List<Laboratoire>();

        public virtual ICollection<Laboratoire> Laboratoires_labo { get; set; } = new List<Laboratoire>();
    }
}
