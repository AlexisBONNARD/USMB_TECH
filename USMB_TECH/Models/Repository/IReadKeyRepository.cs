using System.Linq.Expressions;

namespace USMB_TECH.Models.Repository
{
    public interface IReadKeyRepository<TEntity, in TIdentity>
    {
        Task<IEnumerable<TEntity>> GetByKeysAsync<TProperty>(
            Expression<Func<TEntity, TProperty>> propertySelector,
            TProperty value);
    }
}



