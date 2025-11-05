using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("photo")]
    public partial class Photo
    {
        [Key]
        [Column("id_photo")]
        public int Id_Photo { get; set; }

        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("nom_photo")]
        [MaxLength(100)]
        public string Nom_Photo { get; set; }

        [Column("url_photo")]
        [MaxLength(500)]
        public string Url_Photo { get; set; }

        [ForeignKey("id_equipement")]
        [InverseProperty(nameof(Equipement.Photos))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("id_plateforme")]
        [InverseProperty(nameof(Plateforme.Photos))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;
    }
}
