namespace USMB_TECH.Models.Repository
{
    public interface IReadRepository<TEntity, in TIdentity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TIdentity id);
    }
}
