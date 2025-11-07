using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class ThematiqueManager : IMainRepository<Thematique, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<IEnumerable<Thematique>> GetAllAsync()
        {
            return await context.Thematiques.ToListAsync();
        }

        public async Task<Thematique?> GetByIdAsync(int id)
        {
            return await context.Thematiques.FindAsync(id);
        }

        public async Task AddAsync(Thematique entity) 
        {
            context.Thematiques.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Thematique entityToUpdate, Thematique entity) 
        {
            context.Thematiques.Attach(entityToUpdate);
            context.Thematiques.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Thematique entity) 
        {
            context.Thematiques.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
