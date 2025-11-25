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
            // Supprime les relations directes de la plateforme
            _context.Presenters.RemoveRange(
                _context.Presenters.Where(pr => pr.Id_Plateforme == entity.Id_Plateforme));
            _context.Specifiers.RemoveRange(
                _context.Specifiers.Where(s => s.Id_Plateforme == entity.Id_Plateforme));
            _context.Exposers.RemoveRange(
                _context.Exposers.Where(ex => ex.Id_Plateforme == entity.Id_Plateforme));
            _context.Gerers.RemoveRange(
                _context.Gerers.Where(g => g.Id_Plateforme == entity.Id_Plateforme));
            _context.Associers.RemoveRange(
                _context.Associers.Where(a => a.Id_Plateforme == entity.Id_Plateforme));
            _context.Prise_Contacts.RemoveRange(
                _context.Prise_Contacts.Where(pc => pc.Id_Plateforme == entity.Id_Plateforme));
            _context.Photos.RemoveRange(
                _context.Photos.Where(ph => ph.Id_Plateforme == entity.Id_Plateforme));
            _context.Exemple_Utilisations.RemoveRange(
                _context.Exemple_Utilisations.Where(eu => eu.Id_Plateforme == entity.Id_Plateforme));

            // Récupérer tous les équipements associés
            var equipements = _context.Equipements.Where(e => e.Id_Plateforme == entity.Id_Plateforme).ToList();

            // Appeler le controller Equipements pour chaque équipement
            foreach (var equip in equipements)
            {
                // Appel HTTP DELETE vers l’API
                var response = await _httpClient.DeleteAsync($"api/Equipements/{equip.Id_Equipement}");
                response.EnsureSuccessStatusCode();
            }

            // Supprimer la plateforme
            _context.Plateformes.Remove(entity);
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
