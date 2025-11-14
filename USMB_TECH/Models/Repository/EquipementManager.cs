using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class EquipementManager : IMainRepository<Equipement, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();

        public async Task<IEnumerable<Equipement>> GetAllAsync()
        {
            return await context.Equipements
                                .Include(e => e.PlateformeNavigation)
                                .Include(e => e.ModeleNavigation)
                                .Include(e => e.Type_EquipementNavigation)
                                .Include(e => e.Consommers)
                                .Include(e => e.Posseders)
                                .Include(e => e.Exemple_Utilisations)
                                .Include(e => e.Referencers)
                                .Include(e => e.Prise_Contacts)
                                .Include(e => e.Photos)
                                .Include(e => e.Fournirs)
                                .ToListAsync();
        }


        public async Task<Equipement?> GetByIdAsync(int id) 
        {
            return await context.Equipements.Include<Equipement, Plateforme>(e => e.PlateformeNavigation)
                                           .Include<Equipement, Modele>(e => e.ModeleNavigation)
                                           .Include<Equipement, Type_Equipement>(e => e.Type_EquipementNavigation)
                                           .Include(e => e.Consommers)
                                           .Include(e => e.Posseders)
                                           .Include(e => e.Exemple_Utilisations)
                                           .Include(e => e.Referencers)
                                           .Include(e => e.Prise_Contacts)
                                           .Include(e => e.Photos)
                                           .Include(e => e.Fournirs)
                                           .FirstOrDefaultAsync(e => e.Id_Equipement == id);
        }

        public async Task AddAsync(Equipement entity) 
        {
            context.Equipements.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipement entityToUpdate, Equipement entity) 
        {
            context.Equipements.Attach(entityToUpdate);
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipement entity) 
        {
            context.Equipements.Remove(entity);
            context.SaveChangesAsync();
        }
    }
}
