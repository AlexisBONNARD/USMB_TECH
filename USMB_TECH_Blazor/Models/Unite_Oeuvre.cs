using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Unite_Oeuvre
    {
        public int Id_Unite_Oeuvre { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nom_Unite_Oeuvre { get; set; }
    }
}
