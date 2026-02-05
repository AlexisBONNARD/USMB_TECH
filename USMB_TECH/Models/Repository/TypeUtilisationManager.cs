using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class TypeUtilisationManager
    {
        private readonly UsmbTechDbContext _context;

        public TypeUtilisationManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type_Utilisation>> GetAllAsync()
        {
            return await _context.Type_Utilisations.ToListAsync();
        }

        public async Task AddAsync(Type_Utilisation entity)
        {
            _context.Type_Utilisations.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
