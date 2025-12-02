using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("domaine_excellence")]
    public class Domaine_Excellence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_domaine_excellence")]
        public int Id_Domaine_Excellence { get; set; }

        [Column("intitule_domaine_excellence")]
        [MaxLength(50)]
        public string? intitule_Domaine_Excellence { get; set; }

        [Column("description_domaine_excellence")]
        [MaxLength(200)]
        public string? Description_Domaine_Excellence { get; set; }

        [InverseProperty(nameof(Photo.Domaine_ExcellenceNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        [InverseProperty(nameof(Pole_Expertise.Domaine_ExcellenceNavigation))]
        public virtual ICollection<Pole_Expertise> Pole_Expertises { get; set; } = new List<Pole_Expertise>();
    }
}
