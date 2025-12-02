using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("gerer")]
    public partial class Gerer
    {
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("pourcentage")]
        [Precision(5, 2)]
        public double Pourcentage { get; set; }

        [ForeignKey("Nom_Court")]
        [InverseProperty(nameof(Laboratoire.Gerers))]
        public virtual Laboratoire? LaboratoireNavigation { get; set; } = null!;

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Gerers))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;
    }
}
