using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("modele")]   
    public partial class Modele
    {
        [Key]
        [Column("id_Modele")]
        public int Id_Modele { get; set; }

        [Column("id_Marque")]
        public int Id_Marque { get; set; }

        [Column("nom_Modele")]
        [MaxLength(50)] 
        public string Nom_Modele { get; set; }

        [InverseProperty(nameof(Equipement.ModeleNavigation))]
        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

        [ForeignKey("id_Marque")]
        [InverseProperty(nameof(Marque.Modeles))]
        public virtual Marque? MarqueNavigation { get; set; } = null!;
    }
}