using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public class Type_Equipement
    {
        public int Id_Type_Equipement { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom Type Equipement ne doit pas dépasser 50 caractères")]
        public string Nom_Type { get; set; }
    }
}
