using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class ProposerManager
    {
        private readonly UsmbTechDbContext _context;

        public ProposerManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proposer>> GetByEquipementAsync(int idEquipement)
        {
            return await _context.Proposers
                .Include(p => p.Type_UtilisationNavigation)
                .Where(p => p.Id_Equipement == idEquipement)
                .ToListAsync();
        }

        public async Task AddAsync(int idEquipement, int idTypeUtilisation)
        {
            var exists = await _context.Proposers
                .AnyAsync(p => p.Id_Equipement == idEquipement &&
                               p.Id_Type_Utilisation == idTypeUtilisation);

            if (!exists)
            {
                _context.Proposers.Add(new Proposer
                {
                    Id_Equipement = idEquipement,
                    Id_Type_Utilisation = idTypeUtilisation
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idEquipement, int idTypeUtilisation)
        {
            var entity = await _context.Proposers
                .FirstOrDefaultAsync(p => p.Id_Equipement == idEquipement &&
                                          p.Id_Type_Utilisation == idTypeUtilisation);

            if (entity != null)
            {
                _context.Proposers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
