using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("gerer")]
    public partial class Gerer
    {
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("pourcentage")]
        [Precision(5, 2)]
        public double Pourcentage { get; set; }
    }
}
