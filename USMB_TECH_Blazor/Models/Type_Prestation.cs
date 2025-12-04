using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public partial class Type_Prestation
    {
        public int Id_Type_Prestation { get; set; }
        public string Nom_Type_Prestation { get; set; }
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();


    }
}
