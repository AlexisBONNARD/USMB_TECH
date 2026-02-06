using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Type_ClientManager : IMainRepository<Type_Client, int>
    {
        private readonly UsmbTechDbContext _context;

        public Type_ClientManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type_Client>> GetAllAsync()
        {
            return await _context.Type_Clients.ToListAsync();
        }

        public async Task<Type_Client?> GetByIdAsync(int id)
        {
            return await _context.Type_Clients
                .FirstOrDefaultAsync(tc => tc.Id_Type_Client == id);
        }

        public async Task AddAsync(Type_Client entity)
        {
            await _context.Type_Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Type_Client entityToUpdate, Type_Client entity)
        {
            _context.Type_Clients.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Type_Client entity)
        {
            _context.Type_Clients.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Type_Client>> GetByKeysAsync<TProperty>(
            Expression<Func<Type_Client, TProperty>> propertySelector,
            TProperty value)
        {
            return await _context.Type_Clients
                .Where(p => EF.Property<TProperty>(p,
                    ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Type_Client>> SearchAsync(Expression<Func<Type_Client, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
