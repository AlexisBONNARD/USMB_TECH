using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class FonctionnaliteManager : IMainRepository<Fonctionnalite, int>
    {
        private readonly UsmbTechDbContext _context;

        public FonctionnaliteManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Fonctionnalite?> GetByIdAsync(int id)
        {
            return await _context.Fonctionnalites.FindAsync(id);
        }

        public async Task<IEnumerable<Fonctionnalite>> GetAllAsync()
        {
            return await _context.Fonctionnalites.ToListAsync();
        }

        public async Task AddAsync(Fonctionnalite entity)
        {
            _context.Fonctionnalites.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fonctionnalite entity)
        {
            _context.Fonctionnalites.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fonctionnalite entityToUpdate, Fonctionnalite entity)
        {
            _context.Fonctionnalites.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Fonctionnalite>> GetByKeysAsync<TProperty>(
    Expression<Func<Fonctionnalite, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Fonctionnalites
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Fonctionnalite>> SearchAsync(Expression<Func<Fonctionnalite, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
