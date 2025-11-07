using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PrestationManager : IMainRepository<Prestation, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<IEnumerable<Prestation>> GetAllAsync() 
        {
            return await context.Prestations.ToListAsync();
        }

        public async Task<Prestation?> GetByIdAsync(int id) 
        {
            return await context.Prestations.FindAsync(id);
        }

        public async Task AddAsync(Prestation entity) 
        {
             context.Prestations.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prestation entityToUpdate, Prestation entity) 
        {
            context.Prestations.Attach(entityToUpdate);
            context.Prestations.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prestation entity) 
        {
            context.Prestations.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
