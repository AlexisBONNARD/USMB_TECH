using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.DTO;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class EquipementManager : IMainRepository<Equipement, int>
    {
        private readonly UsmbTechDbContext _context;

        public EquipementManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipement>> GetAllAsync()
        {
            return await _context.Equipements
                .Include(e => e.Pole_ExpertiseNavigation)
                    .ThenInclude(p => p.Specifiers)
                        .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(p => p.Exposers)
                        .ThenInclude(ex => ex.ThematiqueNavigation)
                .Include(e => e.ModeleNavigation)
                .Include(e => e.Type_EquipementNavigation)
                .Include(e => e.Consommers)
                .Include(e => e.Posseders)
                .Include(e => e.Exemple_Utilisations)
                .Include(e => e.Referencers)
                .Include(e => e.Prise_Contacts)
                .Include(e => e.Photos)
                .Include(e => e.Fournirs)
                    .ThenInclude(f => f.PrestationNavigation)
                        .ThenInclude(p => p.Type_PrestationNavigation)
                .ToListAsync();
        }

        public async Task<Equipement?> GetByIdAsync(int id)
        {
            return await _context.Equipements
                .Include(e => e.Pole_ExpertiseNavigation)
                    .ThenInclude(p => p.Photos)
                .Include(e => e.Pole_ExpertiseNavigation)
                    .ThenInclude(p => p.Specifiers)
                        .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(p => p.Exposers)
                        .ThenInclude(ex => ex.ThematiqueNavigation)
                .Include(e => e.ModeleNavigation)
                    .ThenInclude(m => m.MarqueNavigation)
                .Include(e => e.Type_EquipementNavigation)
                .Include(e => e.Consommers)
                .Include(e => e.Posseders)
                .Include(e => e.Exemple_Utilisations)
                .Include(e => e.Referencers)
                .Include(e => e.Prise_Contacts)
                .Include(e => e.Photos)
                .Include(e => e.Fournirs)
                    .ThenInclude(f => f.PrestationNavigation)
                        .ThenInclude(p => p.Type_PrestationNavigation)
                .Include(p => p.Pole_ExpertiseNavigation)
                    .ThenInclude(de => de.Domaine_ExcellenceNavigation)
                .Include(e => e.Exemple_Utilisations)

                // ✅ Fournirs with their Prestation
                .Include(e => e.Fournirs)
                    .ThenInclude(f => f.PrestationNavigation)
                        .ThenInclude(p => p.Type_PrestationNavigation)
                .FirstOrDefaultAsync(e => e.Id_Equipement == id);


        }


        public async Task AddAsync(Equipement entity)
        {
            if (entity.Pole_ExpertiseNavigation != null &&
                !string.IsNullOrEmpty(entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise))
            {
                var pole_expertise = await _context.Pole_Expertises
                    .FirstOrDefaultAsync(p => p.Nom_Pole_Expertise == entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise);

                if (pole_expertise == null)
                {
                    pole_expertise = new Pole_Expertise
                    {
                        Nom_Pole_Expertise = entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise
                    };

                    _context.Pole_Expertises.Add(pole_expertise);
                    await _context.SaveChangesAsync();
                }

                entity.Id_Pole_Expertise = pole_expertise.Id_Pole_Expertise;
                entity.Pole_ExpertiseNavigation = pole_expertise;
            }

            if (entity.Type_EquipementNavigation != null &&
                !string.IsNullOrEmpty(entity.Type_EquipementNavigation.Nom_Type))
            {
                var type = await _context.Type_Equipements
                    .FirstOrDefaultAsync(t => t.Nom_Type == entity.Type_EquipementNavigation.Nom_Type);

                if (type == null)
                {
                    type = new Type_Equipement
                    {
                        Nom_Type = entity.Type_EquipementNavigation.Nom_Type
                    };

                    _context.Type_Equipements.Add(type);
                    await _context.SaveChangesAsync();
                }

                entity.Id_Type_Equipement = type.Id_Type_Equipement;
                entity.Type_EquipementNavigation = type;
            }
            if (entity.ModeleNavigation.MarqueNavigation is not null)
            {
                var marqueName = entity.ModeleNavigation.MarqueNavigation.Nom_Marque;
                var existingMarque = await _context.Marques
                    .FirstOrDefaultAsync(m => m.Nom_Marque == marqueName);

                if (existingMarque is not null)
                {
                    entity.ModeleNavigation.Id_Marque = existingMarque.Id_Marque;
                    entity.ModeleNavigation.MarqueNavigation = existingMarque;
                }
                else
                {
                    var newMarque = new Marque
                    {
                        Nom_Marque = marqueName
                    };
                    _context.Marques.Add(newMarque);
                    await _context.SaveChangesAsync();
                    entity.ModeleNavigation.Id_Marque = newMarque.Id_Marque;
                    entity.ModeleNavigation.MarqueNavigation = newMarque;
                }
            }
            if (entity.ModeleNavigation != null && !string.IsNullOrEmpty(entity.ModeleNavigation.Nom_Modele))
            {
                var model = await _context.Modeles.FirstOrDefaultAsync(m => m.Nom_Modele == entity.ModeleNavigation.Nom_Modele);

                if (model == null)
                {
                    model = new Modele
                    {
                        Nom_Modele = entity.ModeleNavigation.Nom_Modele,
                        Id_Marque = entity.ModeleNavigation.Id_Marque,
                        MarqueNavigation = entity.ModeleNavigation.MarqueNavigation
                    };
                    _context.Modeles.Add(model);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Modele = model.Id_Modele;
                entity.ModeleNavigation = model;
            }
            _context.Equipements.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipement entityToUpdate, Equipement updatedEntity)
        {
            // --- Propriétés simples ---
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            // --- Photos ---
            // Supprimer les photos existantes associées au domaine
            var existingPhotos = await _context.Photos
                .Where(p => p.Id_Equipement == entityToUpdate.Id_Equipement)
                .ToListAsync();

            _context.Photos.RemoveRange(existingPhotos);

            // Mettre à jour le domaine d'excellence
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            // Ajouter les nouvelles photos (si elles existent dans l'entité mise à jour)
            foreach (var photo in updatedEntity.Photos)
            {
                _context.Photos.Add(new Photo
                {
                    Nom_Photo = photo.Nom_Photo,
                    Url_Photo = photo.Url_Photo,
                    Id_Equipement = entityToUpdate.Id_Equipement
                });
            }

            // Sauvegarder les changements
            await _context.SaveChangesAsync();

            // --- Exemple_Utilisations ---
            foreach (var updatedEx in updatedEntity.Exemple_Utilisations)
            {
                Console.WriteLine("ID reçu : " + updatedEx.Id_Exemple_Utilisation);

                var existingEx = entityToUpdate.Exemple_Utilisations
                    .FirstOrDefault(eu => eu.Id_Exemple_Utilisation == updatedEx.Id_Exemple_Utilisation);

                if (existingEx != null)
                {
                    existingEx.Nom_Utilisation = updatedEx.Nom_Utilisation;
                    existingEx.Description_Utilisation = updatedEx.Description_Utilisation;
                }
                else
                {
                    updatedEx.Id_Equipement = entityToUpdate.Id_Equipement;
                    entityToUpdate.Exemple_Utilisations.Add(updatedEx);
                }
            }

            // --- Fournirs ---
            foreach (var updatedF in updatedEntity.Fournirs)
            {
                var existingF = entityToUpdate.Fournirs
                    .FirstOrDefault(f => f.Id_Equipement == updatedF.Id_Equipement &&
                                         f.Id_Prestation == updatedF.Id_Prestation);

                if (existingF == null)
                {
                    updatedF.Id_Equipement = entityToUpdate.Id_Equipement;
                    entityToUpdate.Fournirs.Add(updatedF);
                }
                else
                {
                    // Mise à jour manuelle si tu as des propriétés scalaires à modifier
                    // ex: existingF.Quantite = updatedF.Quantite;
                }
            }

            // ⚠️ Pas de suppression automatique → les données existantes restent en base

            await _context.SaveChangesAsync();
        }





        public async Task DeleteAsync(Equipement entity)
        {
            _context.Fournirs.RemoveRange(
                _context.Fournirs.Where(f => f.Id_Equipement == entity.Id_Equipement));
            _context.Photos.RemoveRange(
                _context.Photos.Where(p => p.Id_Equipement == entity.Id_Equipement));
            _context.Prise_Contacts.RemoveRange(
                _context.Prise_Contacts.Where(pc => pc.Id_Equipement == entity.Id_Equipement));
            _context.Referencers.RemoveRange(
                _context.Referencers.Where(r => r.Id_Equipement == entity.Id_Equipement));
            _context.Exemple_Utilisations.RemoveRange(
                _context.Exemple_Utilisations.Where(eu => eu.Id_Equipement == entity.Id_Equipement));
            _context.Posseders.RemoveRange(
                _context.Posseders.Where(p => p.Id_Equipement == entity.Id_Equipement));
            _context.Consommers.RemoveRange(
                _context.Consommers.Where(c => c.Id_Equipement == entity.Id_Equipement));

            _context.Equipements.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Equipement>> GetByKeysAsync<TProperty>(
    Expression<Func<Equipement, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Equipements
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public async Task<IEnumerable<Equipement>> SearchAsync(Expression<Func<Equipement, bool>> predicate)
        {
            return await _context.Equipements
                .Include(e => e.Pole_ExpertiseNavigation)
                    .ThenInclude(p => p.Specifiers)
                        .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(e => e.Exposers)
                    .ThenInclude(t => t.ThematiqueNavigation)
                .Where(predicate)
                .ToListAsync();
        }

    }
}