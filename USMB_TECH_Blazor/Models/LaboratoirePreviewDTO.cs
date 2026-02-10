using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class LaboratoirePreviewDTO
    {
        public string Nom_Court { get; set; }
        [Required(ErrorMessage = "Le Laboratoire actuelle n'a pas reçu de nom")]
        [MaxLength(255, ErrorMessage = "Le Nom long Laboratoire ne doit pas dépasser 255 caractères")]
        public string Nom_Long { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        [MaxLength(1000, ErrorMessage = "La Description Laboratoire ne doit pas dépasser 1000 caractères")]
        public string Description { get; set; }
        public List<string> MotsCles { get; set; } = new();
        public List<string> Thematiques { get; set; } = new();
    }

}
