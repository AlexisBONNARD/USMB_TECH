using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Laboratoire
    {
        public string Nom_Court { get; set; }

        public int Id_Adresse_Campus { get; set; }

        public int Id_Adresse_Labo { get; set; }

        public string Nom_Long { get; set; }

        public string Description { get; set; }
    }
}
