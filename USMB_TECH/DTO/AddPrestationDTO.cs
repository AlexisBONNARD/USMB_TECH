using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.DTO
{
    public class AddPrestationDTO
    {

        public int Id_Prestation { get; set; }
        public string Intitule_Prestation { get; set; }

        public string? Nom_Court { get; set; }

        public string Nom_Contact { get; set; }

        public string Nom_Domaine { get; set; }

        public string Type { get; set; }
        public string Unite { get; set; }
        public string Description_Prestation { get; set; }

        public double Prix_Revient { get; set; }
        public double Prix_Vente { get; set; }

        public bool Peux_Ce_Realiser_Chez_Le_Client { get; set; }

        public bool Actif { get; set; }

        public ICollection<string> Mots_Clefs { get; set;}

        public ICollection<string> Poles { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }
    }
}
