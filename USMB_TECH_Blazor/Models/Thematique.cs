using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Thematique
    {
        public int Id_Thematique { get; set; }

        public int? Id_Sous_Thematique { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom_Thematique { get; set; }

        public virtual ICollection<Exposer> Exposers { get; set; } = new List<Exposer>();
    }
}
