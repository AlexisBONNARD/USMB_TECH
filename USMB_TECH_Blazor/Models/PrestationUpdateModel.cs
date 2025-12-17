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

        public string? Nom_Pole_Expertise { get; set; }

        public string? Nom_Domaine { get; set; }

        public string? Type { get; set; }

        public string? Unite { get; set; }

        public int Id_Contact { get; set; }

        public string? Nom_Contact { get; set; }

        public string Intitule_Prestation { get; set; } = string.Empty;

        public string Description_Prestation { get; set; } = string.Empty;

        public double Prix_Revient { get; set; }

        public double Prix_Vente { get; set; }

        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        public bool Actif { get; set; }

        // Liste de mots-clés pour l'UI
        public ICollection<string> Mots_Clefs { get; set; } = new List<string>();

        // Collections liées à l'update mais optionnelles
        public ICollection<Preciser> Precisers { get; set; } = new List<Preciser>();
        public virtual Type_Prestation? Type_PrestationNavigation { get; set; } = null;
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
