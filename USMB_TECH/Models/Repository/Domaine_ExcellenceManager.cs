using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class Domaine_ExcellenceManager : IMainRepository<Domaine_Excellence, int>
    {
        private readonly UsmbTechDbContext _context;

        public Domaine_ExcellenceManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domaine_Excellence>> GetAllAsync()
        {
            return await _context.Domaine_Excellences
                .Include(Photo => Photo.Photos)
                .Include(Pole_Expertise => Pole_Expertise.Pole_Expertises)
                .ToListAsync();
        }
        public async Task<Domaine_Excellence?> GetByIdAsync(int id)
        {
            return await _context.Domaine_Excellences
                .Include(d => d.Photos) // photos du domaine
                .Include(d => d.Pole_Expertises) // les pôles liés
                    .ThenInclude(p => p.Specifiers) // les spécificateurs des pôles
                        .ThenInclude(s => s.Mot_ClefNavigation) // les mots-clés des spécificateurs

                .Include(d => d.Pole_Expertises)
                    .ThenInclude(p => p.Photos) // les photos des pôles

                .Include(d => d.Prestations) // les prestations liées
                    .ThenInclude(pr => pr.Precisers) 
                        .ThenInclude(s => s.Mot_ClefNavigation) 
                .Include(d => d.Prestations)
                    .ThenInclude(Type => Type.Type_PrestationNavigation)

                .Include(p=>p.Prestations)
                        .ThenInclude(pr => pr.Photos) // les photos des prestations

                .FirstOrDefaultAsync(e => e.Id_Domaine_Excellence == id);
        }


        public async Task AddAsync(Domaine_Excellence entity)
        {
            _context.Domaine_Excellences.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domaine_Excellence entity) 
        {
            _context.Domaine_Excellences.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domaine_Excellence entityToUpdate, Domaine_Excellence entity)
        {
            // Attacher l'entité existante
            _context.Domaine_Excellences.Attach(entityToUpdate);

            // Supprimer les photos existantes associées au domaine
            var existingPhotos = await _context.Photos
                .Where(p => p.Id_Domaine_Excellence == entityToUpdate.Id_Domaine_Excellence)
                .ToListAsync();

            _context.Photos.RemoveRange(existingPhotos);

            // Mettre à jour le domaine d'excellence
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            // Ajouter les nouvelles photos (si elles existent dans l'entité mise à jour)
            foreach (var photo in entity.Photos)
            {
                _context.Photos.Add(new Photo
                {
                    Nom_Photo = photo.Nom_Photo,
                    Url_Photo = photo.Url_Photo,
                    Id_Domaine_Excellence = entityToUpdate.Id_Domaine_Excellence // Associer la photo au domaine
                });
            }

            // Sauvegarder les changements
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domaine_Excellence>> GetByKeysAsync<TProperty>(
    Expression<Func<Domaine_Excellence, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Domaine_Excellences
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public async Task<IEnumerable<Domaine_Excellence>> SearchAsync(Expression<Func<Domaine_Excellence, bool>> predicate)
        {
            return await _context.Domaine_Excellences
                .Where(predicate)
                .ToListAsync();
        }

    }
}
