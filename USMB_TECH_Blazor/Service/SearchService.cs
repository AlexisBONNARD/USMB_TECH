using System.Net.Http.Json;
using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Service
{
    public class SearchService
    {
        private readonly HttpClient _http;

        public SearchService(HttpClient http)
        {
            _http = http;
        }

        // 🔍 Recherche globale qui retourne le DTO complet
        public async Task<GlobalSearchResultDTO> GlobalSearchAsync(string query, string mode = "motclef")
        {
            if (string.IsNullOrWhiteSpace(query))
                return new GlobalSearchResultDTO(); // retourne un DTO vide

            try
            {
                var result = await _http.GetFromJsonAsync<GlobalSearchResultDTO>(
                    $"api/Search/global?query={Uri.EscapeDataString(query)}&mode={mode}");

                return result ?? new GlobalSearchResultDTO();
            }
            catch
            {
                return new GlobalSearchResultDTO();
            }
        }

        // 🔍 Si tu veux garder une recherche spécifique aux équipements
        public async Task<IEnumerable<EquipementPreviewDTO>> SearchEquipementsAsync(string query, string mode = "motclef")
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<EquipementPreviewDTO>();

            try
            {
                var results = await _http.GetFromJsonAsync<IEnumerable<EquipementPreviewDTO>>(
                    $"api/Search?query={Uri.EscapeDataString(query)}&mode={mode}");

                return results ?? Enumerable.Empty<EquipementPreviewDTO>();
            }
            catch
            {
                return Enumerable.Empty<EquipementPreviewDTO>();
            }
        }
    }
}
