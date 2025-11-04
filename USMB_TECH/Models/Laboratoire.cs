using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Laboratoire")]
    public class Laboratoire
    {
        [Key]
        [Column("Nom_Court")]
        public int Nom_Court { get; set; }

        [Column("Id_Adresse_Campus")]
        public int Id_Adresse_Campus { get; set; }

        [Column("Id_Adresse_Labo")]
        public int Id_Adresse_Labo { get; set; }

        [Column("Nom_Long")]
        [StringLength(255)]
        public string? Nom_Long { get; set; }

        [Column("Description")]
        [StringLength(1000)]
        public string? Description { get; set; }


    }
}
