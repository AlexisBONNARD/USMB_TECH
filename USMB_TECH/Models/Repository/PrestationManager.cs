using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PrestationManager : IMainRepository<Prestation, int>
    {
        private readonly UsmbTechDbContext _context;

        public PrestationManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prestation>> GetAllAsync()
        {
            return await _context.Prestations.ToListAsync();
        }

        public async Task<Prestation?> GetByIdAsync(int id)
        {
            return await _context.Prestations.FindAsync(id);
        }

        public async Task AddAsync(Prestation entity)
        {
            _context.Prestations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prestation entityToUpdate, Prestation entity)
        {
            _context.Prestations.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prestation entity)
        {
            _context.Prestations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prestation>> GetByKeysAsync<TProperty>(
    Expression<Func<Prestation, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Prestations
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
