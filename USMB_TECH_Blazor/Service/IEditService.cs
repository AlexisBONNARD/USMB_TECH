namespace USMB_TECH_Blazor.Service
{
    public interface IEditService<TEntity, in TIdentity> 
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity updatedEntity);
        Task DeleteAsync(TIdentity id);
    }
}
