using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class MotClefManager : IMainRepository<Mot_Clef, int>
    {
        private readonly UsmbTechDbContext _context;

        public MotClefManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Mot_Clef?> GetByIdAsync(int id)
        {
            return await _context.Mot_Clefs.FindAsync(id);
        }

        public async Task<IEnumerable<Mot_Clef>> GetAllAsync()
        {
            return await _context.Mot_Clefs.ToListAsync();
        }

        public async Task AddAsync(Mot_Clef entity)
        {
            _context.Mot_Clefs.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Mot_Clef entity)
        {
            _context.Mot_Clefs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mot_Clef entityToUpdate, Mot_Clef entity)
        {
            _context.Mot_Clefs.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Mot_Clef>> GetByKeysAsync<TProperty>(
    Expression<Func<Mot_Clef, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Mot_Clefs
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Mot_Clef>> SearchAsync(Expression<Func<Mot_Clef, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
