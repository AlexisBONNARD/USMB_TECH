using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Prise_ContactManager : IMainRepository<Prise_Contact, int>
    {
        private readonly UsmbTechDbContext _context;

        public Prise_ContactManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prise_Contact>> GetAllAsync()
        {
            return await _context.Prise_Contacts
                .Include(p => p.EquipementNavigation)
                .Include(p => p.Pole_ExpertiseNavigation)
                .Include(p => p.Type_ClientNavigation)
                .ToListAsync();
        }

        public async Task<Prise_Contact?> GetByIdAsync(int id)
        {
            return await _context.Prise_Contacts
                // Equipement + photos
                .Include(p => p.EquipementNavigation)
                    .ThenInclude(e => e.Photos)

                // Pôle + ses photos
                .Include(p => p.Pole_ExpertiseNavigation)
                    .ThenInclude(pe => pe.Photos)

                // Pôle → équipements → photos
                .Include(p => p.Pole_ExpertiseNavigation)
                    .ThenInclude(pe => pe.Equipements)
                        .ThenInclude(e => e.Photos)

                // Laboratoire + photos
                .Include(p => p.LaboratoireNavigation)
                    .ThenInclude(l => l.Photos)

                // Type client
                .Include(p => p.Type_ClientNavigation)

                .FirstOrDefaultAsync(p => p.Num_Prise_Contact == id);
        }

        public async Task AddAsync(Prise_Contact entity)
        {
            await _context.Prise_Contacts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Prise_Contact entityToUpdate, Prise_Contact entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prise_Contact entity)
        {
            var prise = await _context.Prise_Contacts
                .FirstOrDefaultAsync(p => p.Num_Prise_Contact == entity.Num_Prise_Contact);

            if (prise == null)
                return;

            _context.Prise_Contacts.Remove(prise);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prise_Contact>> GetByKeysAsync<TProperty>(
            Expression<Func<Prise_Contact, TProperty>> propertySelector,
            TProperty value)
        {
            var propertyName = ((MemberExpression)propertySelector.Body).Member.Name;

            return await _context.Prise_Contacts
                .Where(p => EF.Property<TProperty>(p, propertyName).Equals(value))
                .ToListAsync();
        }

        public Task<IEnumerable<Prise_Contact>> SearchAsync(Expression<Func<Prise_Contact, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
