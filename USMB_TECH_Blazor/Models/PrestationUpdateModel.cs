using System.Collections.Generic;

namespace USMB_TECH_Blazor.Models
{
    public class PrestationUpdateModel
    {
        public int Id_Prestation { get; set; }
        public int Id_Domaine_Excellence { get; set; }
        public int Id_Unite_Oeuvre { get; set; }
        public int Id_Type_Prestation { get; set; }
        public string? Nom_Court { get; set; }
        public int Id_Contact { get; set; }
        public string Intitule_Prestation { get; set; } = string.Empty;
        public string Description_Prestation { get; set; } = string.Empty;
        public double Prix_Revient { get; set; }
        public double Prix_Vente { get; set; }
        public bool Peut_Realiser_Chez_Client { get; set; }
        public bool Actif { get; set; }

        public List<int> MotCleIds { get; set; } = new();

        public List<Preciser> Precisers { get; set; } = new();
    }

}
