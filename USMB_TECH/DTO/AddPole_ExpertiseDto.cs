
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class AddPole_ExpertiseDto
    {
        public string Nom_Pole_Expertise { get; set; }
        public string Description_Pole_Expertise { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }
        public bool Actif { get; set; }

        public int Id_Domaine_Excellence { get; set; }

        public List<Presenter> Presenters { get; set; } = new();
        public List<PhotoDto> Photos { get; set; } = new();
        public List<MotCleDto> MotsCles { get; set; } = new();
        public List<Domaine_Excellence> DomaineExcellences { get; set; } = new();
        public List<ExempleUtilisationDto> ExempleUtilisations { get; set; } = new();
        public List<EquipementDTO> Equipements { get; set; } = new();
    }
}
