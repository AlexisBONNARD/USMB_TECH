using System.Net.Http.Json;

using USMB_TECH_Blazor.Models;

namespace USMB_TECH_Blazor.Models
{
    public class SearchService
    {
        private readonly HttpClient _http;

        public SearchService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Equipement>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<Equipement>();

            try
            {
                // Appel GET à l'API avec query en paramètre
                var results = await _http.GetFromJsonAsync<IEnumerable<Equipement>>($"api/Search?query={Uri.EscapeDataString(query)}");
                return results ?? Enumerable.Empty<Equipement>();
            }
            catch
            {
                // En cas d'erreur, on retourne une liste vide
                return Enumerable.Empty<Equipement>();
            }
        }
    }
}
