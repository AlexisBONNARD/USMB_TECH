using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("laboratoire")]
    public partial class Laboratoire
    {
        [Key]
        [Column("nom_court")]
        [MaxLength(25)]
        public string Nom_Court { get; set; }

        [Column("id_adresse_campus")]
        public int Id_Adresse_Campus { get; set; }

        [Column("id_adresse_labo")]
        public int Id_Adresse_Labo { get; set; }

        [Column("nom_long")]
        [MaxLength(255)]
        public string Nom_Long { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("id_adresse_campus")]
        [InverseProperty(nameof(Adresse.Laboratoires_campus))]
        public virtual Adresse? Adresse_campusNavigation { get; set; } = null!;

        [ForeignKey("id_adresse_labo")]
        [InverseProperty(nameof(Adresse.Laboratoires_labo))]
        public virtual Adresse? Adresse_laboNavigation { get; set; } = null!;

        [InverseProperty(nameof(Prestation.LaboratoireNavigation))]
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();

        [InverseProperty(nameof(Contact_USMB.LaboratoireNavigation))]
        public virtual ICollection<Contact_USMB> Contacts { get; set; } = new List<Contact_USMB>();

        [InverseProperty(nameof(Designer.LaboratoireNavigation))]
        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();
    }
}
