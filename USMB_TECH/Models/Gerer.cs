using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("Gerer")]
    public partial class Gerer
    {
        [Column("Nom_Court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("Id_Plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("Pourcentage")]
        [Precision(5, 2)]
        public double Pourcentage { get; set; }
    }
}
