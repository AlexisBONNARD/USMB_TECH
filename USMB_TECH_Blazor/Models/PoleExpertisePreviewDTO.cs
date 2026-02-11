using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class PoleExpertisePreviewDTO
    {
        public int Id_Pole_Expertise { get; set; }
        [Required(ErrorMessage = "Le nom de la pole_expertise est obligatoire")]
        [MaxLength(50, ErrorMessage = "Le nom pole Expertise ne doit pas dépasser 50 caractères")]
        public string Nom_Pole_Expertise { get; set; }
        [Required(ErrorMessage = "La description est obligatoire")]
        [MaxLength(250, ErrorMessage = "La Description Pole Expertise ne doit pas dépasser 250 caractères")]
        public string Description { get; set; }
        public List<string> MotsCles { get; set; } = new();
    }

}
