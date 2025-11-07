using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class LaboratoireManager : IMainRepository<Laboratoire, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<Laboratoire?> GetByIdAsync(int id) 
        {
            return await context.Laboratoires.FindAsync(id);
        }

        public async Task<IEnumerable<Laboratoire>> GetAllAsync() 
        {
            return await context.Laboratoires.ToListAsync();
        }

        public async Task AddAsync(Laboratoire entity)
        {
            context.Laboratoires.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Laboratoire entity) 
        {
            context.Laboratoires.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Laboratoire entity) 
        {
            context.Laboratoires.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task PutAsync(Laboratoire entityToUpdate, Laboratoire entity) 
        {
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }
    }
}
