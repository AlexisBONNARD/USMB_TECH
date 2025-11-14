using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PlateformeManager : IMainRepository<Plateforme, int>
    {
        private readonly UsmbTechDbContext _context;

        public PlateformeManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plateforme>> GetAllAsync()
        {
            return await _context.Plateformes.ToListAsync();
        }

        public async Task<Plateforme?> GetByIdAsync(int id)
        {
            return await _context.Plateformes
                .Include(p => p.Specifiers)
                    .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(p => p.Presenters)
                    .ThenInclude(pr => pr.PrestationNavigation)
                .FirstOrDefaultAsync(p => p.Id_Plateforme == id);
        }

        public async Task AddAsync(Plateforme entity)
        {
            _context.Plateformes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plateforme entityToUpdate, Plateforme entity)
        {
            _context.Plateformes.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Plateforme entity)
        {
            _context.Plateformes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
