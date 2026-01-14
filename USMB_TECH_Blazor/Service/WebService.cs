using System.Net.Http.Json;
using System.Reflection;

namespace USMB_TECH_Blazor.Service
{
    public class WebService<TEntity, TIdentity>: IMainService<TEntity,TIdentity>
    {
        private readonly HttpClient _httpClient;
        private string _endpoint;
        public WebService(string endpoint) 
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/")
            };
            this._endpoint = endpoint;
        }
        public async Task AddAsync(TEntity entity)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_endpoint}", entity);
            if (!response.IsSuccessStatusCode) 
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur API: {error}");
            }
        }

        public async Task DeleteAsync(TIdentity id) 
        {
            await _httpClient.DeleteAsync($"{_endpoint}/{id}");
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"{_endpoint}");
        }
        public async Task<TEntity?> GetByIdAsync(TIdentity id) 
        {
            return await _httpClient.GetFromJsonAsync<TEntity?>($"{_endpoint}/{id}");
        }

        public async Task UpdateAsync(TEntity updatedEntity) 
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty("Id");
            if(propertyInfo != null) throw new InvalidOperationException("L'entité ne possède pas de propriété Id");
            var id = propertyInfo.GetValue(updatedEntity);
            await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", updatedEntity);
        }
    }
}
