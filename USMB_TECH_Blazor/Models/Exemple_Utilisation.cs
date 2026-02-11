using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    public class Exemple_Utilisation
    {
        public int Id_Exemple_Utilisation { get; set; }

        public int? Id_Equipement { get; set; }

        public int? Id_Pole_Expertise { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Utilisation ne doit pas dépasser 50 caractères")]
        public string Nom_Utilisation { get; set; }

        [MaxLength(500, ErrorMessage = "La Description Utilisation ne doit pas dépasser 500 caractères")]
        public string Description_Utilisation { get; set; }
    }
}
