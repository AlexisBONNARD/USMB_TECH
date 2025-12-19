using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Unite_OeuvreManager : IMainRepository<Unite_Oeuvre, int>
    {
        private readonly UsmbTechDbContext _context;

        public Unite_OeuvreManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Unite_Oeuvre?> GetByIdAsync(int id)
        {
            return await _context.Unite_Oeuvres.FindAsync(id);
        }

        public async Task<IEnumerable<Unite_Oeuvre>> GetAllAsync()
        {
            return await _context.Unite_Oeuvres.ToListAsync();
        }

        public async Task AddAsync(Unite_Oeuvre entity)
        {
            _context.Unite_Oeuvres.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Unite_Oeuvre entity)
        {
            _context.Unite_Oeuvres.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Unite_Oeuvre entityToUpdate, Unite_Oeuvre entity)
        {
            _context.Unite_Oeuvres.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Unite_Oeuvre>> GetByKeysAsync<TProperty>(
    Expression<Func<Unite_Oeuvre, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Unite_Oeuvres
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Unite_Oeuvre>> SearchAsync(Expression<Func<Unite_Oeuvre, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
