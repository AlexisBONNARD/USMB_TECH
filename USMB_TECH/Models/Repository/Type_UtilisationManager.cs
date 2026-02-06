using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Type_UtilisationManager : IMainRepository<Type_Utilisation, int>
    {
        private readonly UsmbTechDbContext _context;

        public Type_UtilisationManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type_Utilisation>> GetAllAsync()
        {
            return await _context.Type_Utilisations.ToListAsync();
        }

        public async Task<Type_Utilisation?> GetByIdAsync(int id)
        {
            return await _context.Type_Utilisations
                .FirstOrDefaultAsync(tu => tu.Id_Type_Utilisation == id);
        }

        public async Task AddAsync(Type_Utilisation entity)
        {
            await _context.Type_Utilisations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Type_Utilisation entityToUpdate, Type_Utilisation entity)
        {
            _context.Type_Utilisations.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Type_Utilisation entity)
        {
            _context.Type_Utilisations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Type_Utilisation>> GetByKeysAsync<TProperty>(
            Expression<Func<Type_Utilisation, TProperty>> propertySelector,
            TProperty value)
        {
            return await _context.Type_Utilisations
                .Where(p => EF.Property<TProperty>(p,
                    ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Type_Utilisation>> SearchAsync(Expression<Func<Type_Utilisation, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
