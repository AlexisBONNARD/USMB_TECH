using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Plateforme
    {
        public int Id_Plateforme { get; set; }

        public string Nom_Plateforme { get; set; }

        public string Description_Plateforme { get; set; }

        public string Nom_Contenu { get; set; }

        public string Url_Contenu { get; set; }

        public string Description_Contenu { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
    }
}
