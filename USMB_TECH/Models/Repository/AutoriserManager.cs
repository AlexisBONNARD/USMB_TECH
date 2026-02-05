using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class AutoriserManager
    {
        private readonly UsmbTechDbContext _context;

        public AutoriserManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autoriser>> GetByEquipementAsync(int idEquipement)
        {
            return await _context.Autorisers
                .Include(a => a.Type_ClientNavigation)
                .Where(a => a.Id_Equipement == idEquipement)
                .ToListAsync();
        }

        public async Task AddAsync(int idEquipement, int idTypeClient)
        {
            var exists = await _context.Autorisers
                .AnyAsync(a => a.Id_Equipement == idEquipement &&
                               a.Id_Type_Client == idTypeClient);

            if (!exists)
            {
                _context.Autorisers.Add(new Autoriser
                {
                    Id_Equipement = idEquipement,
                    Id_Type_Client = idTypeClient
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idEquipement, int idTypeClient)
        {
            var entity = await _context.Autorisers
                .FirstOrDefaultAsync(a => a.Id_Equipement == idEquipement &&
                                          a.Id_Type_Client == idTypeClient);

            if (entity != null)
            {
                _context.Autorisers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
