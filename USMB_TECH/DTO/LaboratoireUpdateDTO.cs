using System.ComponentModel.DataAnnotations;
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
    }

}
