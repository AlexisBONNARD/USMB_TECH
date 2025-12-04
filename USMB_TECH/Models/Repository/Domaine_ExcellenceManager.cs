using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Domaine_ExcellenceManager : IMainRepository<Domaine_Excellence, int>
    {
        private readonly UsmbTechDbContext _context;

        public Domaine_ExcellenceManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domaine_Excellence>> GetAllAsync()
        {
            return await _context.Domaine_Excellences
                .Include(Photo => Photo.Photos)
                .Include(Pole_Expertise => Pole_Expertise.Pole_Expertises)
                .ToListAsync();
        }
        public async Task<Domaine_Excellence?> GetByIdAsync(int id)
        {
            return await _context.Domaine_Excellences
                .Include(d => d.Photos) // photos du domaine
                .Include(d => d.Pole_Expertises) // les pôles liés
                    .ThenInclude(p => p.Specifiers) // les spécificateurs des pôles
                        .ThenInclude(s => s.Mot_ClefNavigation) // les mots-clés des spécificateurs

                .Include(d => d.Pole_Expertises)
                    .ThenInclude(p => p.Photos) // les photos des pôles


                .FirstOrDefaultAsync(e => e.Id_Domaine_Excellence == id);
        }


        public async Task AddAsync(Domaine_Excellence entity)
        {
            _context.Domaine_Excellences.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domaine_Excellence entity) 
        {
            _context.Domaine_Excellences.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domaine_Excellence entityToUpdate, Domaine_Excellence entity) 
        {
            _context.Domaine_Excellences.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Domaine_Excellence>> GetByKeysAsync<TProperty>(
    Expression<Func<Domaine_Excellence, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Domaine_Excellences
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
