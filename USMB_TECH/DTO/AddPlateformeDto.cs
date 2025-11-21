
using USMB_TECH.Models;

namespace USMB_TECH.DTO
{
    public class AddPlateformeDto
    {
        public string Nom_Plateforme { get; set; }
        public string Description_Plateforme { get; set; }
        public string Nom_Contenu { get; set; }
        public string Url_Contenu { get; set; }
        public string Description_Contenu { get; set; }
        public bool Actif { get; set; }

        public List<Presenter> Presenters { get; set; } = new();
        public List<PhotoDto> Photos { get; set; } = new();
        public List<MotCleDto> MotsCles { get; set; } = new();
        public List<ThematiqueDto> Thematiques { get; set; } = new();
        public List<ExempleUtilisationDto> ExempleUtilisations { get; set; } = new();
        public List<EquipementDTO> Equipements { get; set; } = new();
    }
}
