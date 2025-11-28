using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("posseder")]
    public partial class Posseder
    {
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_fonctionalite")]
        public int Id_Fonctionalite { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Posseders))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;
    
        [ForeignKey("Id_Fonctionalite")]
        [InverseProperty(nameof(Fonctionalite.Posseders))]
        public virtual Fonctionalite? FonctionaliteNavigation { get; set; } = null!;
    }
}
