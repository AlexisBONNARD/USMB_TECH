using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH_Blazor.Models
{
    public partial class Gerer
    {
        public string Nom_Court { get; set; }

        public int Id_Pole_Expertise { get; set; }

        [Range(0, 999.99, ErrorMessage = "Le Pourcentage doit avoir 5 chiffres max dont 2 max après la virgule")]
        [Precision(5, 2)]
        public double Pourcentage { get; set; }

        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;
    }
}
