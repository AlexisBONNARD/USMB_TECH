using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("presenter")]
    public partial class Presenter
    {
        [Column("id_pole_expertise")]
        public int Id_Pole_Expertise { get; set; }

        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Presenters))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;

        [ForeignKey("Id_Prestation")]
        [InverseProperty(nameof(Prestation.Presenters))]
        public virtual Prestation? PrestationNavigation { get; set; } = null!;
    }
}
