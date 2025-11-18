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

        public async Task<IEnumerable<EquipementPreviewDTO>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<EquipementPreviewDTO>();

            try
            {
                var results = await _http.GetFromJsonAsync<IEnumerable<EquipementPreviewDTO>>(
                    $"api/Search?query={Uri.EscapeDataString(query)}");

                return results ?? Enumerable.Empty<EquipementPreviewDTO>();
            }
            catch
            {
                return Enumerable.Empty<EquipementPreviewDTO>();
            }
        }
    }
}
