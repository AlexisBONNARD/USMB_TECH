using System.ComponentModel.DataAnnotations;

namespace USMB_TECH_Blazor.Models
{
    public class Mot_Clef

    {
        public int Id_Mot_Clef { get; set; }

        [Required(ErrorMessage = "Le Mot Clef actuelle n'a pas reçu de nom")]
        [MaxLength(25, ErrorMessage = "Le Nom Mot Clef ne doit pas dépasser 25 caractères")]
        public string Nom_Mot_Clef { get; set; }

        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();

        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();
    }
}
