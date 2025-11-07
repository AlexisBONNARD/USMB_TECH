using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class EquipementManager : IMainRepository<Equipement, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<IEnumerable<Equipement>> GetAllAsync() 
        {
            return await context.Equipements.ToListAsync();
        }

        public async Task<Equipement?> GetByIdAsync(int id) 
        {
            return await context.Equipements.FindAsync(id);
        }

        public async Task AddAsync(Equipement entity) 
        {
            context.Equipements.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipement entityToUpdate, Equipement entity) 
        {
            context.Equipements.Attach(entityToUpdate);
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipement entity) 
        {
            context.Equipements.Remove(entity);
            context.SaveChangesAsync();
        }
    }
}
