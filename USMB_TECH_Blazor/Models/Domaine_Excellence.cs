namespace USMB_TECH_Blazor.Models
{
    public class Domaine_Excellence
    {
        public int Id_Domaine_Excellence { get; set; }
        public string? intitule_Domaine_Excellence { get; set; }
        public string? Description_Domaine_Excellence { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public virtual ICollection<Pole_Expertise> Pole_Expertises { get; set; } = new List<Pole_Expertise>();
    }
}
