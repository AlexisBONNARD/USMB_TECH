
using System.ComponentModel.DataAnnotations;
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class AddPole_ExpertiseDto
    {
        public int Id_Pole_Expertise { get; set; }

        public string Nom_Pole_Expertise { get; set; }

        public string Description_Pole_Expertise { get; set; }

        public string Nom_Contenu { get; set; }

        public string Url_Contenu { get; set; }

        public string Description_Contenu { get; set; }
        public bool Actif { get; set; }

        public int Id_Domaine_Excellence { get; set; }

        public virtual ICollection<string> MotsCles { get; set; }
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();

        public virtual ICollection<Presenter> Presenters { get; set; } = new List<Presenter>();

        public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
        public virtual ICollection<Exemple_Utilisation> ExempleUtilisations { get; set; } = new List<Exemple_Utilisation>();

        public virtual ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
