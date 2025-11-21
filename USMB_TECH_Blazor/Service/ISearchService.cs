using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Service
{
    public interface ISearchService
    {
        Task<IEnumerable<EquipementPreviewDTO>> GlobalSearchAsync(string query);
    }
}