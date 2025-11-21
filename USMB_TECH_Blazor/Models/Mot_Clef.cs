namespace USMB_TECH_Blazor.Models
{
    public class Mot_Clef

    {
        public int Id_Mot_Clef { get; set; }

        public string Nom_Mot_Clef { get; set; }

        public virtual ICollection<Designer> Designers { get; set; } = new List<Designer>();

        public virtual ICollection<Specifier> Specifiers { get; set; } = new List<Specifier>();
    }
}
