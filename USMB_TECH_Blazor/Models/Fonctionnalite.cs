using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Fonctionnalite
    {
        public int Id_Fonctionalite { get; set; }

        public string Nom_Fonctionalite { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();
    }
}
