using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class LaboratoireManager : IMainRepository<Laboratoire, string>
    {
        private readonly UsmbTechDbContext _context;

        public LaboratoireManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Laboratoire?> GetByIdAsync(string id) 
        {
            return await _context.Laboratoires.FindAsync(id);
        }

        public async Task<IEnumerable<Laboratoire>> GetAllAsync() 
        {
            return await _context.Laboratoires.ToListAsync();
        }

        public async Task AddAsync(Laboratoire entity)
        {
            _context.Laboratoires.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Laboratoire entity) 
        {
            _context.Laboratoires.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Laboratoire entityToUpdate, Laboratoire entity) 
        {
            _context.Laboratoires.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
