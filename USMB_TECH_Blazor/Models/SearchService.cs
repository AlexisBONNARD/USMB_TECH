using System.Net.Http.Json;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Service
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _httpClient;

        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Equipement>> GlobalSearchAsync(string query)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Equipement>>(
                $"api/search?query={query}"
            );
        }
    }
}
