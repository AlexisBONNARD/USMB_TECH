using USMB_TECH_Blazor.Models;

namespace USMB_TECH.DTO
{
public class GlobalSearchResultDTO
{
    public List<EquipementPreviewDTO> Equipements { get; set; } = new();
    public List<PoleExpertisePreviewDTO> PolesExpertise { get; set; } = new();
    public List<PrestationPreviewDTO> Prestations { get; set; } = new();
    public List<LaboratoirePreviewDTO> Laboratoires { get; set; } = new();
    public List<DomaineExcellenceDTO> DomainesExcellence { get; set; } = new();
}

}
