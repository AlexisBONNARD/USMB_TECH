using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class LaboratoireManager : IMainRepository<Laboratoire, string>
    {
        private readonly UsmbTechDbContext _context;

        public LaboratoireManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Laboratoire?> GetByIdAsync(string id) 
        {
            return await _context.Laboratoires
                .Include(l => l.Adresse_campusNavigation)
                .Include(l => l.Adresse_laboNavigation)
                .Include(l => l.Gerers)
                    .ThenInclude(plt => plt.Pole_ExpertiseNavigation)
                .Include(c => c.Contacts)
                .Include(l => l.Prestations)
                .FirstOrDefaultAsync(lab => lab.Nom_Court == id);
        }

        public async Task<IEnumerable<Laboratoire>> GetAllAsync() 
        {
            return await _context.Laboratoires.ToListAsync();
        }

        public async Task AddAsync(Laboratoire entity)
        {
            _context.Laboratoires.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Laboratoire entity) 
        {
            _context.Laboratoires.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Laboratoire entityToUpdate, Laboratoire entity) 
        {
            _context.Laboratoires.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Laboratoire>> GetByKeysAsync<TProperty>(
    Expression<Func<Laboratoire, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Laboratoires
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
