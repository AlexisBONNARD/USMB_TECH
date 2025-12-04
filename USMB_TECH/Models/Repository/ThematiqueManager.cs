using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class ThematiqueManager : IMainRepository<Thematique, int>
    {
        private readonly UsmbTechDbContext _context;

        public ThematiqueManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Thematique>> GetAllAsync()
        {
            return await _context.Thematiques
                .Include(p => p.Exposers)
                    .ThenInclude(e => e.EquipementNavigation)
                        .ThenInclude(p => p.Photos)
                .ToListAsync();
        }

        public async Task<Thematique?> GetByIdAsync(int id)
        {
            return await _context.Thematiques.FindAsync(id);
        }

        public async Task AddAsync(Thematique entity)
        {
            _context.Thematiques.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Thematique entityToUpdate, Thematique entity)
        {
            _context.Thematiques.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Thematique entity)
        {
            _context.Thematiques.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Thematique>> GetByKeysAsync<TProperty>(
    Expression<Func<Thematique, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Thematiques
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
