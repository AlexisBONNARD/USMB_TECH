using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PlateformeManager : IMainRepository<Plateforme, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();
       

        public async Task<IEnumerable<Plateforme>> GetAllAsync()
        {
            return await context.Plateformes.ToListAsync();
        }
        public async Task<Plateforme?> GetByIdAsync(int id)
        {
            return await context.Plateformes.FindAsync(id);
        }
        public async Task UpdateAsync(Plateforme entityToUpdate, Plateforme entity)
        {
            context.Plateformes.Attach(entityToUpdate);
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

                public async Task AddAsync(Plateforme entity)
        {
            context.Plateformes.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Plateforme entity)
        {
            context.Plateformes.Remove(entity);
            await context.SaveChangesAsync();
        }

    }
}
