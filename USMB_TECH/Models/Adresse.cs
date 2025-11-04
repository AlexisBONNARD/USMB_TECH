using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Adresse")]
    public class Adresse
    {
        [Key]
        [Column("Id_Adresse")]
        public int Id_Adresse { get; set; }

        [Column("Rue_Adresse")]
        [StringLength(200)]
        public string? Rue_Adresse { get; set; }

        [Column("Complement_Rue_Adresse")]
        [StringLength(200)]
        public string? Complement_Rue_Adresse { get; set; }

        [Column("Code_Postal_Adresse")]
        [StringLength(11)]
        public string? Code_Postal_Adresse { get; set; }

        [Column("Ville_Adresse")]
        [StringLength(100)]
        public string? Ville_Adresse { get; set; }

        [Column("Pays_Adresse")]
        [StringLength(50)]
        public string? Pays_Adresse { get; set; }
    }
}
