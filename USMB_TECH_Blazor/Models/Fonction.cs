using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Fonction
    {
        public int Id_Fonction { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Fonction ne doit pas dépasser 50 caractères")]
        public string Nom_Fonction { get; set; }
    }
}
