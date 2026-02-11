using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Fonctionnalite
    {
        public int Id_Fonctionnalite { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Fonctionnalite ne doit pas dépasser 50 caractères")]
        public string Nom_Fonctionnalite { get; set; }

        [MaxLength(1000, ErrorMessage = "La Description Fonctionnalite ne doit pas dépasser 1000 caractères")]
        public string Description { get; set; }

        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();
    }
}
