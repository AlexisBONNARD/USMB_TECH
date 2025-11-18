using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
                .Include(e => e.Exposers)
                    .ThenInclude(t => t.ThematiqueNavigation)
                .Include(p => p.Equipements)
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

        public async Task<IEnumerable<Plateforme>> GetByKeysAsync<TProperty>(
            Expression<Func<Plateforme, TProperty>> propertySelector,
            TProperty value)
        {
            return await _context.Plateformes
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
