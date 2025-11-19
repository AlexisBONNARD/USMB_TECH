using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("adresse")]
    public partial class Adresse
    {
        
        [Column("id_adresse")]
        public int Id_Adresse { get; set; }

        [Column("rue_adresse")]
        [MaxLength(200)]
        public string? Rue_Adresse { get; set; }

        [Column("complement_rue_adresse")]
        [MaxLength(200)]
        public string? Complement_Rue_Adresse { get; set; }

        [Column("code_postal_adresse")]
        [MaxLength(11)]
        public string? Code_Postal_Adresse { get; set; }

        [Column("ville_adresse")]
        [MaxLength(100)]
        public string? Ville_Adresse { get; set; }

        [Column("pays_adresse")]
        [MaxLength(50)]
        public string? Pays_Adresse { get; set; }

        [InverseProperty(nameof(Laboratoire.Adresse_campusNavigation))]
        public virtual ICollection<Laboratoire> Laboratoires_campus { get; set; } = new List<Laboratoire>();

        [InverseProperty(nameof(Laboratoire.Adresse_laboNavigation))]
        public virtual ICollection<Laboratoire> Laboratoires_labo { get; set; } = new List<Laboratoire>();
    }
}
