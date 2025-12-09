using System.Linq.Expressions;

namespace USMB_TECH.Models.Repository
{
    public interface IReadRepository<TEntity, in TIdentity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TIdentity id);

        Task<IEnumerable<TEntity>> GetByKeysAsync<TProperty>(
            Expression<Func<TEntity, TProperty>> propertySelector,
            TProperty value);

        Task<IEnumerable<TEntity>> SearchAsync(
            Expression<Func<TEntity, bool>> predicate);
    }
}
