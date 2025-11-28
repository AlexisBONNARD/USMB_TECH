namespace USMB_TECH_Blazor.Service
{
    public interface IMainService<TEntity, in TIdentity> : IReadService<TEntity, TIdentity>, IEditService<TEntity, TIdentity>
    { 
    }
}
