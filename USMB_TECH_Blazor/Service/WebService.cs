using System.Net.Http.Json;
using System.Reflection;

namespace USMB_TECH_Blazor.Service
{
    public class WebService<TEntity, TIdentity>: IMainService<TEntity,TIdentity>
    {
        private readonly HttpClient _httpClient;
        public WebService() 
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7093/")
            };
        }
        public async Task AddAsync(TEntity entity) 
        {
            await _httpClient.PostAsJsonAsync($"", entity);
        }

        public async Task DeleteAsync(TIdentity id) 
        {
            await _httpClient.DeleteAsync($"");
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"");
        }
        public async Task<TEntity?> GetByIdAsync(TIdentity id) 
        {
            return await _httpClient.GetFromJsonAsync<TEntity?>($"");
        }

        public async Task UpdateAsync(TEntity updatedEntity) 
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty("Id");
            if(propertyInfo != null) throw new InvalidOperationException("L'entité ne possède pas de propriété Id");
            var id = propertyInfo.GetValue(updatedEntity);
            await _httpClient.PutAsJsonAsync($"", updatedEntity);
        }
    }
}
