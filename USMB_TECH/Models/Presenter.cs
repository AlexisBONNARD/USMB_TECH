using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("presenter")]
    public partial class Presenter
    {
        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("id_prestation")]
        public int Id_Prestation { get; set; }

        [ForeignKey("id_plateforme")]
        [InverseProperty(nameof(Plateforme.Presenters))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("id_prestation")]
        [InverseProperty(nameof(Prestation.Presenters))]
        public virtual Prestation? PrestationNavigation { get; set; } = null!;
    }
}
