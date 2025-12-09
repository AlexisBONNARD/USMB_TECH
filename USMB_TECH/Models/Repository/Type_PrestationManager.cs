using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;
namespace USMB_TECH.Models.Repository
{
    public class Type_PrestationManager : IMainRepository<Type_Prestation, int>
    {
        private readonly UsmbTechDbContext _context;
        
        public Type_PrestationManager(UsmbTechDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Type_Prestation>> GetAllAsync() 
        {
            return await _context.Type_Prestations.ToListAsync();
        }
        public async Task<Type_Prestation?> GetByIdAsync(int id) 
        {
            return await _context.Type_Prestations.FindAsync(id);
        }

        public async Task AddAsync(Type_Prestation entity) 
        {
            _context.Type_Prestations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Type_Prestation entity) 
        {
            _context.Type_Prestations.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Type_Prestation entityToUpdate, Type_Prestation entity) 
        {
            _context.Type_Prestations.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Type_Prestation>> GetByKeysAsync<TProperty>(
   Expression<Func<Type_Prestation, TProperty>> propertySelector,
   TProperty value)
        {
            return await _context.Type_Prestations
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Type_Prestation>> SearchAsync(Expression<Func<Type_Prestation, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}