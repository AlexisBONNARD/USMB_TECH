namespace USMB_TECH_Blazor.Service
{
    public interface IReadService<TEntity, in TIdentity>
    {
        Task<IEnumerable<TEntity?>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TIdentity id);
    }
}
