using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PlateformeManager : IMainRepository<Plateforme, int>
    {
        public UsmbTechDbContext context = new UsmbTechDbContext();
       

        public async Task<IEnumerable<Plateforme>> GetAllAsync()
        {
            return await context.Plateformes.ToListAsync();
        }
        public async Task<Plateforme?> GetByIdAsync(int id)
        {
            return await context.Plateformes
                .Include(p => p.Specifiers)
                    .ThenInclude(s => s.Mot_ClefNavigation)
                    .Include(p => p.Presenters)
                    .ThenInclude(pr => pr.PrestationNavigation)
                .FirstOrDefaultAsync(p => p.Id_Plateforme == id);
        }

        public async Task UpdateAsync(Plateforme entityToUpdate, Plateforme entity)
        {
            context.Plateformes.Attach(entityToUpdate);
            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }

                public async Task AddAsync(Plateforme entity)
        {
            context.Plateformes.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Plateforme entity)
        {
            context.Plateformes.Remove(entity);
            await context.SaveChangesAsync();
        }
        // Méthode générique pour ajouter n'importe quelle entité liée
        public async Task AddEntityAsync<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        // Méthode générique pour sauvegarder toutes les modifications
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
