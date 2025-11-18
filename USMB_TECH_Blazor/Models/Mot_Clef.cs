using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public partial class Mot_Clef
    {
        public int Id_Mot_Clef { get; set; }

        public string Nom_Mot_Clef { get; set; }

        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();

        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();
    }
}