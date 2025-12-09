using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class MarqueManager : IMainRepository<Marque, int>
    {
        private readonly UsmbTechDbContext _context;

        public MarqueManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Marque?> GetByIdAsync(int id) 
        {
            return await _context.Marques.FindAsync(id);
        }

        public async Task<IEnumerable<Marque>> GetAllAsync() 
        {
            return await _context.Marques.ToListAsync();
        }

        public async Task AddAsync(Marque entity)
        {
            _context.Marques.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marque entity) 
        {
            _context.Marques.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marque entityToUpdate, Marque entity) 
        {
            _context.Marques.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Marque>> GetByKeysAsync<TProperty>(
    Expression<Func<Marque, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Marques
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Marque>> SearchAsync(Expression<Func<Marque, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
