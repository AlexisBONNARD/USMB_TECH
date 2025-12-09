using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Prestation
    {
        public int Id_Prestation { get; set; }

        public int Id_Unite_Oeuvre { get; set; }

        public int Id_Type_Prestation { get; set; }

        public string Nom_Court { get; set; }

        public string Nom_Domaine { get; set; }

        public string Type { get; set; }

        public string Unite { get; set; }

        public int Id_Contact { get; set; }

        public string Nom_Contact  { get; set; }

        public string Intitule_Prestation { get; set; }

        public string Description_Prestation { get; set; }

        public double Prix_Revient { get; set; }

        public double Prix_Vente { get; set; }

        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        public bool Actif { get; set; } = true;

        public List<string> Mots_Clefs { get; set; } = new List<string>();

        public List<string> Poles { get; set; } = new List<string>();

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();

        public virtual ICollection<Preciser> Precisers { get; set; } = new List<Preciser>();

        public virtual Type_Prestation? Type_PrestationNavigation { get; set; } = null!;

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
