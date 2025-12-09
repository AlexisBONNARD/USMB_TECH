using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;
namespace USMB_TECH.Models.Repository
{
    public class FonctionManager : IMainRepository<Fonction, int>
    {
        private readonly UsmbTechDbContext _context;
        
        public FonctionManager(UsmbTechDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Fonction>> GetAllAsync() 
        {
            return await _context.Fonctions.ToListAsync();
        }
        public async Task<Fonction?> GetByIdAsync(int id) 
        {
            return await _context.Fonctions.FindAsync(id);
        }

        public async Task AddAsync(Fonction entity) 
        {
            _context.Fonctions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Fonction entity) 
        {
            _context.Fonctions.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Fonction entityToUpdate, Fonction entity) 
        {
            _context.Fonctions.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fonction>> GetByKeysAsync<TProperty>(
   Expression<Func<Fonction, TProperty>> propertySelector,
   TProperty value)
        {
            return await _context.Fonctions
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}