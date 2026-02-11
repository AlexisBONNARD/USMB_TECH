using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Type_Utilisation
    {
        public int Id_Type_Utilisation { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Type Utilisation ne doit pas dépasser 50 caractères")]
        public string Nom_Type_Utilisation { get; set; }

        public virtual ICollection<Proposer> Proposers { get; set; } = new List<Proposer>();
    }

    public class TypeUtilisationSelection
    {
        public int Id_Type_Utilisation { get; set; }
        public string Nom_Type_Utilisation { get; set; }
        public bool IsSelected { get; set; }
    }
}
