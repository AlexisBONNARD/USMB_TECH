using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class PrestationPreviewDTO
    {
        public int Id_Prestation { get; set; }
        [Required(ErrorMessage = "La prestation actuelle n'a pas reçu de nom")]
        [MaxLength(100, ErrorMessage = "L'Intitule Prestation ne doit pas dépasser 100 caractères")]
        public string Intitule_Prestation { get; set; }

        [Required(ErrorMessage = "Une description doit être donnée pour la prestation")]
        [MaxLength(500, ErrorMessage = "La Description Prestation ne doit pas dépasser 500 caractères")]
        public string Description_Prestation { get; set; }
        public List<string> MotsCles { get; set; } = new();
    }

}
