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
                    .ThenInclude(plt => plt.PlateformeNavigation)
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

            if(entity.Adresse_laboNavigation != null) 
            {
                var AdresseLabo = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Rue_Adresse == entity.Adresse_laboNavigation.Rue_Adresse &&
                    a.Code_Postal_Adresse == entity.Adresse_laboNavigation.Code_Postal_Adresse &&
                    a.Ville_Adresse == entity.Adresse_laboNavigation.Ville_Adresse &&
                    a.Pays_Adresse == entity.Adresse_laboNavigation.Pays_Adresse);
                if(AdresseLabo is null) 
                {
                    AdresseLabo = new Adresse()
                    {
                        Rue_Adresse =  entity.Adresse_laboNavigation.Rue_Adresse,
                        Code_Postal_Adresse = entity.Adresse_laboNavigation.Code_Postal_Adresse,
                        Ville_Adresse = entity.Adresse_laboNavigation.Ville_Adresse,
                        Pays_Adresse = entity.Adresse_laboNavigation.Pays_Adresse
                    };
                    _context.Adresses.Add(AdresseLabo);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Adresse_Labo = AdresseLabo.Id_Adresse;
                entity.Adresse_laboNavigation = AdresseLabo;
            }
            if (entity.Adresse_campusNavigation != null)
            {
                var AdresseCampus = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Rue_Adresse == entity.Adresse_campusNavigation.Rue_Adresse &&
                    a.Code_Postal_Adresse == entity.Adresse_campusNavigation.Code_Postal_Adresse &&
                    a.Ville_Adresse == entity.Adresse_campusNavigation.Ville_Adresse &&
                    a.Pays_Adresse == entity.Adresse_campusNavigation.Pays_Adresse);
                if (AdresseCampus is null)
                {
                    AdresseCampus = new Adresse()
                    {
                        Rue_Adresse = entity.Adresse_campusNavigation.Rue_Adresse,
                        Code_Postal_Adresse = entity.Adresse_campusNavigation.Code_Postal_Adresse,
                        Ville_Adresse = entity.Adresse_campusNavigation.Ville_Adresse,
                        Pays_Adresse = entity.Adresse_campusNavigation.Pays_Adresse
                    };
                    _context.Adresses.Add(AdresseCampus);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Adresse_Campus = AdresseCampus.Id_Adresse;
                entity.Adresse_campusNavigation = AdresseCampus;
            }
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
