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

        [Required(ErrorMessage = "Le nom du contenu est obligatoire")]
        public string Nom_Contenu { get; set; }

        [Required(ErrorMessage = "L’URL du contenu est obligatoire")]
        [Url(ErrorMessage = "Veuillez entrer une URL valide")]
        public string Url_Contenu { get; set; }

        [Required(ErrorMessage = "La description du contenu est obligatoire")]
        public string Description_Contenu { get; set; }
        public bool Actif { get; set; }

        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public virtual ICollection<Mot_Clef> MotsCles { get; set; } = new List<Mot_Clef>();

        public virtual ICollection<Exemple_Utilisation> ExempleUtilisations { get; set; } = new List<Exemple_Utilisation>();
        public virtual Domaine_Excellence? Domaine_ExcellenceNavigation { get; set; } = null!;
    }
}
