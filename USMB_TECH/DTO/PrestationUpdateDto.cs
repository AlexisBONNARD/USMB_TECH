using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class PrestationUpdateDto
    {
        public int Id_Prestation { get; set; }
        public string Intitule_Prestation { get; set; }
        public string Description_Prestation { get; set; }
        public decimal Prix_Ht { get; set; }
        public decimal Prix_Tva { get; set; }
        public bool Peut_Realiser_Chez_Client { get; set; }
        public bool Actif { get; set; }

        public int Id_Unite_Oeuvre { get; set; }
        public int Id_Type_Prestation { get; set; }
        public int Id_Domaine_Excellence { get; set; }

        public string Nom_Court { get; set; }

        public int Id_Contact { get; set; }

        public List<int> MotCleIds { get; set; } = new();

        public List<Preciser> Precisers { get; set; } = new();
    }



}
