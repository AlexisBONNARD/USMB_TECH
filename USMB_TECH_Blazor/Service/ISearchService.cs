using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Service
{
    public interface ISearchService
    {
        Task<IEnumerable<Equipement>> GlobalSearchAsync(string query);
    }
}