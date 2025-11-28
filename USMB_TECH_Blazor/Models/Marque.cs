using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Marque
    {
        public int Id_Marque { get; set; }

        public string Nom_Marque { get; set; }
        public virtual ICollection<Modele> Modeles { get; set; } = new List<Modele>();
    }
}
