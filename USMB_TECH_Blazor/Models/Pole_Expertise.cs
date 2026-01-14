using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using USMB_TECH.Models;

namespace USMB_TECH_Blazor.Models
{
    public class Pole_Expertise
    {
        public int Id_Pole_Expertise { get; set; }

        [Required(ErrorMessage = "Le nom de la pole_expertise est obligatoire")]
        public string Nom_Pole_Expertise { get; set; }

        [Required(ErrorMessage = "La description est obligatoire")]
        public string Description_Pole_Expertise { get; set; }

        public bool Actif { get; set; }

        public int Id_Domaine_Excellence { get; set; }

        public virtual ICollection<string> MotsCles { get; set; } = new List<string>();
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();


        public virtual ICollection<Exemple_Utilisation> Exemple_Utilisations { get; set; } = new List<Exemple_Utilisation>();
        public virtual Domaine_Excellence? Domaine_ExcellenceNavigation { get; set; } = null!;
    }
}
