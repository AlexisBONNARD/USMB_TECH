using System.ComponentModel.DataAnnotations;
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class LaboratoireUpdateDTO
    {
        public string Nom_Court { get; set; }

        public int Id_Adresse_Campus { get; set; }

        public int Id_Adresse_Labo { get; set; }
        public string Nom_Long { get; set; }
        public string Description { get; set; }
        public ICollection<int> Pole_Expertises { get; set; } = new List<int>();

        public Adresse? Adresse_campusNavigation { get; set; } = null!;
        public Adresse? Adresse_laboNavigation { get; set; } = null!;

        public List<int> mot_Clefs { get; set; } = new();

        public ICollection<int> Thematiques { get; set; } = new List<int>();

        public List<Est_Lier> Est_Liers { get; set; }
        public List<Gerer> Gerers { get; set; }
        public List<Designer> Designers { get; set; }
        public ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
    }
}
