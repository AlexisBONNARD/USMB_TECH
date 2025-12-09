using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Pole_ExpertiseManager : IMainRepository<Pole_Expertise, int>
    {
        private readonly UsmbTechDbContext _context;
        private readonly HttpClient _httpClient;

        public Pole_ExpertiseManager(UsmbTechDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Pole_Expertise>> GetAllAsync()
        {
            return await _context.Pole_Expertises.ToListAsync();
        }

        public async Task<Pole_Expertise?> GetByIdAsync(int id)
        {
            return await _context.Pole_Expertises
                .Include(p => p.Specifiers)
                    .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(p => p.Presenters)
                    .ThenInclude(pr => pr.PrestationNavigation)

                .Include(p => p.Photos)
                .Include(p => p.Equipements)
                    .ThenInclude(e => e.Photos)
                .Include(Do => Do.Domaine_ExcellenceNavigation)
                .FirstOrDefaultAsync(p => p.Id_Pole_Expertise == id);

        }

        public async Task AddAsync(Pole_Expertise entity)
        {
            _context.Pole_Expertises.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pole_Expertise entityToUpdate, Pole_Expertise updatedEntity)
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
                    updatedEquip.Pole_ExpertiseNavigation = entityToUpdate; //  important
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

        public async Task DeleteAsync(Pole_Expertise entity)
        {
            // Récupérer la pole_expertise avec toutes ses relations
            var pole_expertise = await _context.Pole_Expertises
                .Include(p => p.Associers)
                .Include(p => p.Gerers)
                .Include(p => p.Presenters)
                .Include(p => p.Specifiers)
                .Include(p => p.Photos)
                .Include(p => p.Exemple_Utilisations)
                .Include(p => p.Prise_Contacts)
                .FirstOrDefaultAsync(p => p.Id_Pole_Expertise == entity.Id_Pole_Expertise);

            if (pole_expertise == null) return;

            // Supprimer les collections explicitement pour éviter tout problème
            _context.Associers.RemoveRange(pole_expertise.Associers);
            _context.Gerers.RemoveRange(pole_expertise.Gerers);
            _context.Presenters.RemoveRange(pole_expertise.Presenters);
            _context.Specifiers.RemoveRange(pole_expertise.Specifiers);
            _context.Photos.RemoveRange(pole_expertise.Photos);
            _context.Exemple_Utilisations.RemoveRange(pole_expertise.Exemple_Utilisations);
            _context.Prise_Contacts.RemoveRange(pole_expertise.Prise_Contacts);

            // Supprimer la pole_expertise
            _context.Pole_Expertises.Remove(pole_expertise);

            // EF cascade supprimera automatiquement les Equipements et leurs dépendances
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pole_Expertise>> GetByKeysAsync<TProperty>(
            Expression<Func<Pole_Expertise, TProperty>> propertySelector,
            TProperty value)
        {
            return await _context.Pole_Expertises
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pole_Expertise>> SearchAsync(Expression<Func<Pole_Expertise, bool>> predicate)
        {
            return await _context.Pole_Expertises
                .Where(predicate)
                .ToListAsync();
        }

    }
}
