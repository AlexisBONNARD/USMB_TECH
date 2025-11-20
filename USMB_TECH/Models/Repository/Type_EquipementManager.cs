using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;
namespace USMB_TECH.Models.Repository
{
    public class Type_EquipementManager : IMainRepository<Type_Equipement, int>
    {
        private readonly UsmbTechDbContext _context;
        
        public Type_EquipementManager(UsmbTechDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Type_Equipement>> GetAllAsync() 
        {
            return await _context.Type_Equipements.ToListAsync();
        }
        public async Task<Type_Equipement?> GetByIdAsync(int id) 
        {
            return await _context.Type_Equipements.FindAsync(id);
        }

        public async Task AddAsync(Type_Equipement entity) 
        {
            _context.Type_Equipements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Type_Equipement entity) 
        {
            _context.Type_Equipements.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Type_Equipement entityToUpdate, Type_Equipement entity) 
        {
            _context.Type_Equipements.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Type_Equipement>> GetByKeysAsync<TProperty>(
   Expression<Func<Type_Equipement, TProperty>> propertySelector,
   TProperty value)
        {
            return await _context.Type_Equipements
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}