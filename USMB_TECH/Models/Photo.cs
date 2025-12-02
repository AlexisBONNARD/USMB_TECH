using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("photo")]
    public partial class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_photo")]
        public int Id_Photo { get; set; }

        [Column("id_equipement")]
        public int? Id_Equipement { get; set; }

        [Column("id_pole_expertise")]
        public int? Id_Pole_Expertise { get; set; }

        [Column("nom_photo")]
        [MaxLength(100)]
        public string Nom_Photo { get; set; }

        [Column("url_photo")]
        [MaxLength(500)]
        public string Url_Photo { get; set; }

        [ForeignKey("Id_Equipement")]
        [InverseProperty(nameof(Equipement.Photos))]
        public virtual Equipement? EquipementNavigation { get; set; } = null!;

        [ForeignKey("Id_Pole_Expertise")]
        [InverseProperty(nameof(Pole_Expertise.Photos))]
        public virtual Pole_Expertise? Pole_ExpertiseNavigation { get; set; } = null!;
    }
}
