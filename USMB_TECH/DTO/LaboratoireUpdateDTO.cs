using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class LaboratoireUpdateDTO
    {
        public string Nom_Court { get; set; }
        public string Nom_Long { get; set; }
        public string Description { get; set; }

        public int Id_Adresse_Campus { get; set; }
        public int Id_Adresse_Labo { get; set; }

        public List<int> Pole_Expertises { get; set; } = new();
        public List<int> mot_Clefs { get; set; } = new();
        public List<int> Thematiques { get; set; } = new();

        public Adresse? Adresse_campusNavigation { get; set; }
        public Adresse? Adresse_laboNavigation { get; set; }
        public ICollection<Gerer> Gerers { get; set; } = new List<Gerer>();

        public ICollection<Est_Lier> Est_Liers { get; set; } = new List<Est_Lier>();

        public ICollection<Designer> Designers { get; set; } = new List<Designer>();
    }

}
