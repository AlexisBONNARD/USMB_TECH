using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Contact_USMBManager : IMainRepository<Contact_USMB, int>
    {
        private readonly UsmbTechDbContext _context;

        public Contact_USMBManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact_USMB>> GetAllAsync() 
        {
            return await _context.Contact_USMBs.ToListAsync();
        }

        public async Task<Contact_USMB?> GetByIdAsync(int id)
        {
            return await _context.Contact_USMBs.FindAsync(id);
        }

        public async Task AddAsync(Contact_USMB entity) 
        {
            await _context.Contact_USMBs.AddAsync(entity);
            _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact_USMB entityToUpdate, Contact_USMB entity) 
        {
            _context.Contact_USMBs.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact_USMB entity) 
        {
            _context.Contact_USMBs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact_USMB>> GetByKeysAsync<TProperty>(
    Expression<Func<Contact_USMB, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Contact_USMBs
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Contact_USMB>> SearchAsync(Expression<Func<Contact_USMB, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
