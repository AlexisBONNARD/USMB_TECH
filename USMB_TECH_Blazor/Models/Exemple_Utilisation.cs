using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    public class Exemple_Utilisation
    {
        public int Id_Exemple_Utilisation { get; set; }

        public int? Id_Equipement { get; set; }

        public int? Id_Pole_Expertise { get; set; }

        public string Nom_Utilisation { get; set; }

        public string Description_Utilisation { get; set; }
    }
}
