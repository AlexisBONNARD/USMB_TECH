using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Domaine_Excellence
    {
        public int Id_Domaine_Excellence { get; set; }
        [Required(ErrorMessage = "L'intitulé du domaine d'excellence est obligatoire.")]
        public string? Intitule_Domaine_Excellence { get; set; }
        [Required(ErrorMessage = "La description du domaine d'excellence est obligatoire.")]
        public string? Description_Domaine_Excellence { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public virtual ICollection<Pole_Expertise> Pole_Expertises { get; set; } = new List<Pole_Expertise>();
        public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();
        public virtual ICollection<Mot_Clef> MotsCles { get; set; } = new List<Mot_Clef>();
        public virtual ICollection<Preciser> Precisers { get; set; } = new List<Preciser>();
        public virtual Type_Prestation? Type_PrestationNavigation { get; set; }





    }
}
