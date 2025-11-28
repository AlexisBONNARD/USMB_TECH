using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{ 
    public class Modele
    {
        public int Id_Modele { get; set; }

        public int Id_Marque { get; set; }
        public string Nom_Modele { get; set; }

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
        public virtual Marque? MarqueNavigation { get; set; } = null!;
    }
}