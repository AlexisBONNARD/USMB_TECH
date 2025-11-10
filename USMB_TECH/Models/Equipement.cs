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

        [Column("id_modele")]
        public int Id_Modele { get; set; }

        [Column("id_type_equipement")]  
        public int Id_Type_Equipement { get; set; }

        [Column("nom_equipement")]
        [MaxLength(50)]
        public string Nom_Equipement { get; set; }

        [Column("num_immobilisation")]
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

        [Column("description_technique")]
        [MaxLength(500)]
        public string Description_Technique { get; set; }

        [Column("disponibilite")]
        public bool Disponibilite { get; set; }

        [Column("autonomie")]
        public bool Autonomie { get; set; }

        [Column("utilisable_chez_le_client")]
        public bool Utilisable_Chez_Le_Client { get; set; }

        [Column("actif")]
        public bool Actif { get; set; }

        [ForeignKey("id_plateforme")]
        [InverseProperty(nameof(Plateforme.Equipements))]
        public virtual Plateforme? PlateformeNavigation { get; set; } = null!;

        [ForeignKey("id_modele")]
        [InverseProperty(nameof(Modele.Equipements))]
        public virtual Modele? ModeleNavigation { get; set; } = null!;

        [ForeignKey("id_type_equipement")]
        [InverseProperty(nameof(Type_Equipement.Equipements))]
        public virtual Type_Equipement? Type_EquipementNavigation { get; set; } = null!;

        [InverseProperty(nameof(Consommer.EquipementNavigation))]
        public virtual ICollection<Consommer> Consommers { get; set; } = new List<Consommer>();

        [InverseProperty(nameof(Posseder.EquipementNavigation))]
        public virtual ICollection<Posseder> Posseders { get; set; } = new List<Posseder>();

        [InverseProperty(nameof(Exemple_Utilisation.EquipementNavigation))]
        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();

        [InverseProperty(nameof(Referencer.EquipementNavigation))]
        public virtual ICollection<Referencer> Referencers { get; set; } = new List<Referencer>();

        [InverseProperty(nameof(Prise_Contact.EquipementNavigation))]
        public virtual ICollection<Prise_Contact> Prise_Contacts { get; set; } = new List<Prise_Contact>();

        [InverseProperty(nameof(Photo.EquipementNavigation))]
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        [InverseProperty(nameof(Fournir.EquipementNavigation))]
        public virtual ICollection<Fournir> Fournirs { get; set; } = new List<Fournir>();
    }
}
