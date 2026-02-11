using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public partial class Type_Prestation
    {
        public int Id_Type_Prestation { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Type Prestation ne doit pas dépasser 50 caractères")]
        public string Nom_Type_Prestation { get; set; }

        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();


    }
}
