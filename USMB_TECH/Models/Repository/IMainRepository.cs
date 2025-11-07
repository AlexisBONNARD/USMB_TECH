namespace USMB_TECH.Models.Repository
{
    public interface IMainRepository<TEntity, in TIdentity> : IEditRepository<TEntity>, IReadRepository<TEntity,TIdentity>
    {
    }
}
