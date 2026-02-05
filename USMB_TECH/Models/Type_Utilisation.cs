using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("type_utilisation")]
    public partial class Type_Utilisation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_type_utilisation")]
        public int Id_Type_Utilisation { get; set; }

        [Column("nom_type_utilisation")]
        [MaxLength(50)]
        public string Nom_Type_Utilisation { get; set; }

        [InverseProperty(nameof(Proposer.Type_UtilisationNavigation))]
        public virtual ICollection<Proposer> Proposers { get; set; } = new List<Proposer>();
    }
}
