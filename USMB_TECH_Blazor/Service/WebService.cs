using System.Net.Http.Json;
using System.Reflection;

namespace USMB_TECH_Blazor.Service
{
    public class WebService<TEntity, TIdentity> : IMainService<TEntity, TIdentity>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public WebService(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = "api/" + endpoint;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_endpoint);

            if (!response.IsSuccessStatusCode)
            {
                var raw = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur API {_endpoint} : {raw}");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<TEntity>>();
        }

        public async Task AddAsync(TEntity entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TEntity?> GetByIdAsync(TIdentity id)
        {
            return await _httpClient.GetFromJsonAsync<TEntity>($"{_endpoint}/{id}");
        }

        public async Task UpdateAsync(TEntity updatedEntity)
        {
            var idProperty = typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(p =>
                    p.Name.StartsWith("Id_", StringComparison.OrdinalIgnoreCase)
                    || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                );

            if (idProperty == null)
                throw new InvalidOperationException(
                    $"Aucune propriété Id trouvée sur {typeof(TEntity).Name}"
                );

            var id = idProperty.GetValue(updatedEntity);

            var response = await _httpClient.PutAsJsonAsync(
                $"{_endpoint}/{id}",
                updatedEntity
            );

            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteAsync(TIdentity id)
        {
            await _httpClient.DeleteAsync($"{_endpoint}/{id}");
        }
    }
}
