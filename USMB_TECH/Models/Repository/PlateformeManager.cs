using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PlateformeManager : IMainRepository<Plateforme, int>
    {
        private readonly UsmbTechDbContext _context;
        private readonly HttpClient _httpClient;

        public PlateformeManager(UsmbTechDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Plateforme>> GetAllAsync()
        {
            return await _context.Plateformes.ToListAsync();
        }

        public async Task<Plateforme?> GetByIdAsync(int id)
        {
            return await _context.Plateformes
                .Include(p => p.Specifiers)
                    .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(p => p.Presenters)
                    .ThenInclude(pr => pr.PrestationNavigation)
                .Include(e => e.Exposers)
                    .ThenInclude(t => t.ThematiqueNavigation)

                .Include(p => p.Photos)
                .Include(p => p.Equipements)
                    .ThenInclude(e => e.Photos)

                .FirstOrDefaultAsync(p => p.Id_Plateforme == id);

        }

        public async Task AddAsync(Plateforme entity)
        {
            _context.Plateformes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plateforme entityToUpdate, Plateforme updatedEntity)
        {
            // Propriétés simples
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            // --- Gestion incrémentale des Equipements ---
            foreach (var updatedEquip in updatedEntity.Equipements)
            {
                var existingEquip = entityToUpdate.Equipements
                    .FirstOrDefault(e => e.Id_Equipement == updatedEquip.Id_Equipement);

                if (existingEquip != null)
                {
                    _context.Entry(existingEquip).CurrentValues.SetValues(updatedEquip);
                }
                else
                {
                    updatedEquip.PlateformeNavigation = entityToUpdate; //  important
                    entityToUpdate.Equipements.Add(updatedEquip);
                }
            }

            var toRemove = entityToUpdate.Equipements
                .Where(e => !updatedEntity.Equipements.Any(ue => ue.Id_Equipement == e.Id_Equipement))
                .ToList();

            foreach (var equip in toRemove)
            {
                entityToUpdate.Equipements.Remove(equip);
                _context.Equipements.Remove(equip); //  pour supprimer en base
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Plateforme entity)
        {
            // Récupérer la plateforme avec toutes ses relations
            var plateforme = await _context.Plateformes
                .Include(p => p.Associers)
                .Include(p => p.Exposers)
                .Include(p => p.Gerers)
                .Include(p => p.Presenters)
                .Include(p => p.Specifiers)
                .Include(p => p.Photos)
                .Include(p => p.Exemple_Utilisations)
                .Include(p => p.Prise_Contacts)
                .FirstOrDefaultAsync(p => p.Id_Plateforme == entity.Id_Plateforme);

            if (plateforme == null) return;

            // Supprimer les collections explicitement pour éviter tout problème
            _context.Associers.RemoveRange(plateforme.Associers);
            _context.Exposers.RemoveRange(plateforme.Exposers);
            _context.Gerers.RemoveRange(plateforme.Gerers);
            _context.Presenters.RemoveRange(plateforme.Presenters);
            _context.Specifiers.RemoveRange(plateforme.Specifiers);
            _context.Photos.RemoveRange(plateforme.Photos);
            _context.Exemple_Utilisations.RemoveRange(plateforme.Exemple_Utilisations);
            _context.Prise_Contacts.RemoveRange(plateforme.Prise_Contacts);

            // Supprimer la plateforme
            _context.Plateformes.Remove(plateforme);

            // EF cascade supprimera automatiquement les Equipements et leurs dépendances
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plateforme>> GetByKeysAsync<TProperty>(
            Expression<Func<Plateforme, TProperty>> propertySelector,
            TProperty value)
        {
            return await _context.Plateformes
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }
    }
}
