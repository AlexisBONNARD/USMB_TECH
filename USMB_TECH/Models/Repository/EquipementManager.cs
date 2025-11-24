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
                .Include(e => e.PlateformeNavigation)
                    .ThenInclude(p => p.Specifiers)
                        .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(e => e.PlateformeNavigation)
                    .ThenInclude(p => p.Exposers)
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
                .Include(e => e.PlateformeNavigation)
                    .ThenInclude(p => p.Photos)
                .Include(e => e.PlateformeNavigation)
                    .ThenInclude(p => p.Specifiers)
                        .ThenInclude(s => s.Mot_ClefNavigation)
                .Include(e => e.PlateformeNavigation)
                    .ThenInclude(p => p.Exposers)
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
                .FirstOrDefaultAsync(e => e.Id_Equipement == id);
        }

        public async Task AddAsync(Equipement entity)
        {
            if (entity.PlateformeNavigation != null &&
                !string.IsNullOrEmpty(entity.PlateformeNavigation.Nom_Plateforme))
            {
                var plateforme = await _context.Plateformes
                    .FirstOrDefaultAsync(p => p.Nom_Plateforme == entity.PlateformeNavigation.Nom_Plateforme);

                if (plateforme == null)
                {
                    plateforme = new Plateforme
                    {
                        Nom_Plateforme = entity.PlateformeNavigation.Nom_Plateforme
                    };

                    _context.Plateformes.Add(plateforme);
                    await _context.SaveChangesAsync();
                }

                entity.Id_Plateforme = plateforme.Id_Plateforme;
                entity.PlateformeNavigation = plateforme;
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
            if(entity.ModeleNavigation.MarqueNavigation is not null) 
            {
                var marqueName = entity.ModeleNavigation.MarqueNavigation.Nom_Marque;
                var existingMarque = await _context.Marques
                    .FirstOrDefaultAsync(m => m.Nom_Marque == marqueName);

                if(existingMarque is not null) 
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

            var photos = entity.Photos;
            entity.Photos = null;

            _context.Equipements.Add(entity);
            await _context.SaveChangesAsync();

            foreach (var p in photos)
            {
                p.Id_Equipement = entity.Id_Equipement;
                _context.Photos.Add(p);
            }

            await _context.SaveChangesAsync();


        }


        public async Task UpdateAsync(Equipement entityToUpdate, Equipement entity)
        {
            _context.Equipements.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipement entity)
        {
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
    }
}