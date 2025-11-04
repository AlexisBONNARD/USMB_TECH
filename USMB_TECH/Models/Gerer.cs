using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{

    public class Gerer
    {
        [Column("Nom_Court")]  
        public string Nom_Court { get; set; }

        [Column("Id_Plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("Pourcentage")]
        public double Pourcentage { get; set; }
    }
}
