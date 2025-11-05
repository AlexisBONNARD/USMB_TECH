using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USMB_TECH.Models
{
    [Table("equipement")]
    public partial class Equipement
    {
        [Key]
        [Column("id_equipement")]
        public int Id_Equipement { get; set; }

        [Column("id_plateforme")]
        public int Id_Plateforme { get; set; }

        [Column("id_marque")]
        public int Id_Marque { get; set; }

        [Column("id_type_equipement")]  
        public int Id_Type_Equipement { get; set; }

        [Column("nom_equipement")]
        [MaxLength(50)]
        public string Nom_Equipement { get; set; }

        [Column("num_equipement")]
        [MaxLength(50)]
        public string Num_Immobilisation { get; set; }

        [Column("date_acquisition", TypeName = "date")]
        public DateTime Date_Acquisition { get; set; }

        [Column("prix_achat")]
        [Precision(10, 2)]
        public double Prix_Achat { get; set; }

        [Column("prix_revient")]
        [Precision(10, 2)]
        public double Prix_Revient { get; set; }

        [Column("description_equipement")]
        [MaxLength(500)]
        public string Description_Technique { get; set; }

        [Column("disponibilite")]
        public bool Disponibilite { get; set; }

        [Column("autonomie")]
        public bool Autonomie { get; set; }

        [Column("utiliser_chez_le_client")]
        public bool Utiliser_Chez_Le_Client { get; set; }

        [Column("actif")]
        public bool Actif { get; set; }

        [ForeignKey("id_type_equipement")]
        [InverseProperty(nameof(Type_Equipement.Equipements))]
        public virtual Unite_Oeuvre? Type_EquipementNavigation { get; set; } = null!;

        [ForeignKey("id_plateforme")]
        [InverseProperty(nameof(Plateforme.Equipements))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("id_marque")]
        [InverseProperty(nameof(Marque.Equipements))]
        public virtual Marque? MarqueNavigation { get; set; } = null!;

        [InverseProperty(nameof(Consommer.ConsommerNavigation))]
        public virtual ICollection<Consommer> Consommers { get; set; } = new List<Consommer>();

        [InverseProperty(nameof(Posseder.PossederNavigation))]
        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();

        [InverseProperty(nameof(Exemple_Utilisation.Exemple_UtilisationNavigation))]
        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();

        [InverseProperty(nameof(Referencer.ReferencerNavigation))]
        public virtual ICollection<Referencer> Referencers { get; set; } = new List<Referencer>();

        [InverseProperty(nameof(Prise_Contact.Prise_ContactNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        [InverseProperty(nameof(Photo.PhotoNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        [InverseProperty(nameof(Fournir.FournirNavigation))]
        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();


    }
}
