using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Photo
    {
        public int Id_Photo { get; set; }

        public int? Id_Pole_Expertise { get; set; }

        [MaxLength(100, ErrorMessage = "Le Nom Photo ne doit pas dépasser 100 caractères")]
        public string Nom_Photo { get; set; }

        [MaxLength(500, ErrorMessage = "L'URL Photo ne doit pas dépasser 500 caractères")]
        public string Url_Photo { get; set; }
    }
}
