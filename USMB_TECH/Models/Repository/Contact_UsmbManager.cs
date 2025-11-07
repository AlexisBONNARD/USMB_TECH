using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Contact_USMBManager : IMainRepository<Contact_USMB, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<IEnumerable<Contact_USMB>> GetAllAsync() 
        {
            return await context.Contact_USMBs.ToListAsync();
        }

        public async Task<Contact_USMB?> GetByIdAsync(int id)
        {
            return await context.Contact_USMBs.FindAsync(id);
        }

        public async Task AddAsync(Contact_USMB entity) 
        {
            await context.Contact_USMBs.AddAsync(entity);
            context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact_USMB entityToUpdate, Contact_USMB entity) 
        {
            context.Contact_USMBs.Attach(entityToUpdate);
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact_USMB entity) 
        {
            context.Contact_USMBs.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
