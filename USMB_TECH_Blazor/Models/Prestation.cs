using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Prestation
    {
        public int Id_Prestation { get; set; }

        public int Id_Unite_Oeuvre { get; set; }

        public int Id_Type_Prestation { get; set; }

        public string Nom_Court { get; set; }

        public int Id_Contact { get; set; }


        public string Intitule_Prestation { get; set; }

        public string Description_Prestation { get; set; }

        public double Prix_Revient { get; set; }

        public double Prix_Vente { get; set; }

        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();
    }
}
