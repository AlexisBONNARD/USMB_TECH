using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class LaboratoireManager : IMainRepository<Laboratoire, string>
    {
        private readonly UsmbTechDbContext _context;
        private readonly IMapper _mapper;

        public LaboratoireManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<Laboratoire?> GetByIdAsync(string id)
        {
            return await _context.Laboratoires
                .Include(p => p.Photos)
                .Include(l => l.Adresse_campusNavigation)
                .Include(l => l.Adresse_laboNavigation)
                .Include(l => l.Gerers)
                    .ThenInclude(plt => plt.Pole_ExpertiseNavigation)
                .Include(c => c.Contacts)
                .Include(l => l.Prestations)
                .Include(l => l.Gerers)
                    .ThenInclude(plt => plt.Pole_ExpertiseNavigation)
                        .ThenInclude(p => p.Photos)
                 .Include(el => el.Est_Liers)
                    .ThenInclude(t => t.ThematiqueNavigation)
                 .Include(des => des.Designers)
                 .ThenInclude(mc => mc.Mot_ClefNavigation)
                .FirstOrDefaultAsync(lab => lab.Nom_Court == id);
        }

        public async Task<IEnumerable<Laboratoire>> GetAllAsync()
        {
            return await _context.Laboratoires.ToListAsync();
        }

        public async Task AddAsync(Laboratoire entity)
        {
            if (await _context.Laboratoires.FirstOrDefaultAsync(m => m.Nom_Court == entity.Nom_Court) is not null)
            {
                throw new InvalidOperationException("Un laboratoire avec un nom court similaire est déjà existant." +
                    "\nEssayez un nouveau nom court pour votre laboratoire");
            }

            if (entity.Est_Liers != null && entity.Est_Liers.Any())
            {
                var estlieFinal = new List<Est_Lier>();
                foreach (var estlie in entity.Est_Liers)
                {
                    var thematique = await _context.Thematiques.FirstOrDefaultAsync(t => t.Nom_Thematique == estlie.ThematiqueNavigation.Nom_Thematique);
                    if (thematique == null)
                    {
                        thematique = estlie.ThematiqueNavigation;
                        _context.Thematiques.Add(thematique);
                    }
                    estlieFinal.Add(new Est_Lier
                    {
                        ThematiqueNavigation = thematique,
                        Nom_Court = entity.Nom_Court
                    });
                }
                entity.Est_Liers = estlieFinal;
            }
            if (entity.Designers != null && entity.Designers.Any())
            {
                var designersFinal = new List<Designer>();
                foreach (var designer in entity.Designers)
                {
                    var motClef = await _context.Mot_Clefs.FirstOrDefaultAsync(m => m.Nom_Mot_Clef == designer.Mot_ClefNavigation.Nom_Mot_Clef);

                    if (motClef == null)
                    {
                        motClef = designer.Mot_ClefNavigation;
                        _context.Mot_Clefs.Add(motClef);
                    }
                    designersFinal.Add(new Designer
                    {
                        Mot_ClefNavigation = motClef,
                        Nom_Court = entity.Nom_Court,
                    });
                }
                entity.Designers = designersFinal;
            }
            if (entity.Gerers != null && entity.Gerers.Any())
            {
                var gerersFinal = new List<Gerer>();
                foreach(var gerer in entity.Gerers) 
                {
                    var pole = await _context.Pole_Expertises.FirstOrDefaultAsync(pe => pe.Nom_Pole_Expertise == gerer.Pole_ExpertiseNavigation.Nom_Pole_Expertise);
                    if(pole == null) 
                    {
                        pole = gerer.Pole_ExpertiseNavigation;
                        _context.Pole_Expertises.Add(pole);
                    }
                    gerersFinal.Add(new Gerer
                    {
                        Pole_ExpertiseNavigation = pole,
                        Nom_Court = entity.Nom_Court,
                    });
                }
                entity.Gerers = gerersFinal;
            }
            if (entity.Adresse_laboNavigation.Rue_Adresse == entity.Adresse_campusNavigation.Rue_Adresse
                && entity.Adresse_campusNavigation.Pays_Adresse == entity.Adresse_laboNavigation.Pays_Adresse
                && entity.Adresse_campusNavigation.Code_Postal_Adresse == entity.Adresse_laboNavigation.Code_Postal_Adresse
                && entity.Adresse_laboNavigation.Complement_Rue_Adresse == entity.Adresse_campusNavigation.Complement_Rue_Adresse)
            {
                var adresses = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Rue_Adresse == entity.Adresse_laboNavigation!.Rue_Adresse &&
                    a.Code_Postal_Adresse == entity.Adresse_laboNavigation.Code_Postal_Adresse &&
                    a.Ville_Adresse == entity.Adresse_laboNavigation.Ville_Adresse &&
                    a.Pays_Adresse == entity.Adresse_laboNavigation.Pays_Adresse);
                if (adresses is null)
                {
                    adresses = entity.Adresse_laboNavigation;
                    _context.Adresses.Add(adresses);
                }
                entity.Adresse_laboNavigation = adresses;
                entity.Adresse_campusNavigation = adresses;
            }
            else
            {
                if (entity.Adresse_laboNavigation != null)
                {
                    var adresseLabo = await _context.Adresses.FirstOrDefaultAsync(a =>
                        a.Rue_Adresse == entity.Adresse_laboNavigation.Rue_Adresse &&
                        a.Code_Postal_Adresse == entity.Adresse_laboNavigation.Code_Postal_Adresse &&
                        a.Ville_Adresse == entity.Adresse_laboNavigation.Ville_Adresse &&
                        a.Pays_Adresse == entity.Adresse_laboNavigation.Pays_Adresse);

                    if (adresseLabo is null)
                    {
                        adresseLabo = entity.Adresse_laboNavigation;
                        _context.Adresses.Add(adresseLabo);
                    }

                    entity.Adresse_laboNavigation = adresseLabo;
                }

                if (entity.Adresse_campusNavigation != null)
                {
                    var adresseCampus = await _context.Adresses.FirstOrDefaultAsync(a =>
                        a.Rue_Adresse == entity.Adresse_campusNavigation.Rue_Adresse &&
                        a.Code_Postal_Adresse == entity.Adresse_campusNavigation.Code_Postal_Adresse &&
                        a.Ville_Adresse == entity.Adresse_campusNavigation.Ville_Adresse &&
                        a.Pays_Adresse == entity.Adresse_campusNavigation.Pays_Adresse);

                    if (adresseCampus is null)
                    {
                        adresseCampus = entity.Adresse_campusNavigation;
                        _context.Adresses.Add(adresseCampus);
                    }

                    entity.Adresse_campusNavigation = adresseCampus;
                }
            }
            _context.Laboratoires.Add(entity);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Laboratoire entity)
        {
            _context.Laboratoires.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Laboratoire entityToUpdate, Laboratoire updatedEntity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            if (updatedEntity.Adresse_laboNavigation != null)
            {
                var existingLabo = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Rue_Adresse == updatedEntity.Adresse_laboNavigation.Rue_Adresse &&
                    a.Code_Postal_Adresse == updatedEntity.Adresse_laboNavigation.Code_Postal_Adresse &&
                    a.Ville_Adresse == updatedEntity.Adresse_laboNavigation.Ville_Adresse &&
                    a.Pays_Adresse == updatedEntity.Adresse_laboNavigation.Pays_Adresse &&
                    a.Complement_Rue_Adresse == updatedEntity.Adresse_laboNavigation.Complement_Rue_Adresse);

                if (existingLabo == null)
                {
                    existingLabo = new Adresse
                    {
                        Rue_Adresse = updatedEntity.Adresse_laboNavigation.Rue_Adresse,
                        Ville_Adresse = updatedEntity.Adresse_laboNavigation.Ville_Adresse,
                        Code_Postal_Adresse = updatedEntity.Adresse_laboNavigation.Code_Postal_Adresse,
                        Pays_Adresse = updatedEntity.Adresse_laboNavigation.Pays_Adresse,
                        Complement_Rue_Adresse = updatedEntity.Adresse_laboNavigation.Complement_Rue_Adresse
                    };

                    _context.Adresses.Add(existingLabo);
                    await _context.SaveChangesAsync();
                }

                entityToUpdate.Id_Adresse_Labo = existingLabo.Id_Adresse;
                entityToUpdate.Adresse_laboNavigation = existingLabo;
            }

            if (updatedEntity.Adresse_campusNavigation != null)
            {
                var existingCampus = await _context.Adresses.FirstOrDefaultAsync(a =>
                    a.Rue_Adresse == updatedEntity.Adresse_campusNavigation.Rue_Adresse &&
                    a.Code_Postal_Adresse == updatedEntity.Adresse_campusNavigation.Code_Postal_Adresse &&
                    a.Ville_Adresse == updatedEntity.Adresse_campusNavigation.Ville_Adresse &&
                    a.Pays_Adresse == updatedEntity.Adresse_campusNavigation.Pays_Adresse &&
                    a.Complement_Rue_Adresse == updatedEntity.Adresse_campusNavigation.Complement_Rue_Adresse);

                if (existingCampus == null)
                {
                    existingCampus = new Adresse
                    {
                        Rue_Adresse = updatedEntity.Adresse_campusNavigation.Rue_Adresse,
                        Ville_Adresse = updatedEntity.Adresse_campusNavigation.Ville_Adresse,
                        Code_Postal_Adresse = updatedEntity.Adresse_campusNavigation.Code_Postal_Adresse,
                        Pays_Adresse = updatedEntity.Adresse_campusNavigation.Pays_Adresse,
                        Complement_Rue_Adresse = updatedEntity.Adresse_campusNavigation.Complement_Rue_Adresse
                    };

                    _context.Adresses.Add(existingCampus);
                    await _context.SaveChangesAsync();
                }

                entityToUpdate.Id_Adresse_Campus = existingCampus.Id_Adresse;
                entityToUpdate.Adresse_campusNavigation = existingCampus;
            }

            updatedEntity.Gerers ??= new List<Gerer>();

            foreach (var updatedGerer in updatedEntity.Gerers)
            {
                var exists = entityToUpdate.Gerers
                    .Any(g => g.Id_Pole_Expertise == updatedGerer.Id_Pole_Expertise);

                if (!exists)
                {
                    updatedGerer.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Gerers.Add(updatedGerer);
                }
            }

            var toRemoveGerer = entityToUpdate.Gerers
                .Where(g => !updatedEntity.Gerers
                    .Any(up => up.Id_Pole_Expertise == g.Id_Pole_Expertise))
                .ToList();

            foreach (var gerer in toRemoveGerer)
            {
                entityToUpdate.Gerers.Remove(gerer);
                _context.Gerers.Remove(gerer);
            }

            updatedEntity.Designers ??= new List<Designer>();

            foreach (var updatedDesigner in updatedEntity.Designers)
            {
                var exists = entityToUpdate.Designers
                    .Any(d => d.Id_Mot_Clef == updatedDesigner.Id_Mot_Clef);

                if (!exists)
                {
                    updatedDesigner.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Designers.Add(updatedDesigner);
                }
            }

            var toRemoveDesigner = entityToUpdate.Designers
                .Where(d => !updatedEntity.Designers
                    .Any(up => up.Id_Mot_Clef == d.Id_Mot_Clef))
                .ToList();

            foreach (var designer in toRemoveDesigner)
            {
                entityToUpdate.Designers.Remove(designer);
                _context.Designers.Remove(designer);
            }

            updatedEntity.Est_Liers ??= new List<Est_Lier>();

            foreach (var updatedEstLier in updatedEntity.Est_Liers)
            {
                var existingThematique = await _context.Thematiques
                    .FirstOrDefaultAsync(t => t.Nom_Thematique == updatedEstLier.ThematiqueNavigation.Nom_Thematique);

                if (existingThematique == null)
                {
                    existingThematique = updatedEstLier.ThematiqueNavigation;
                    _context.Thematiques.Add(existingThematique);
                    await _context.SaveChangesAsync();
                }

                var exists = entityToUpdate.Est_Liers
                    .Any(e => e.Id_Thematique == existingThematique.Id_Thematique);

                if (!exists)
                {
                    updatedEstLier.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Est_Liers.Add(updatedEstLier);
                }
            }

            var toRemoveEstLier = entityToUpdate.Est_Liers
                .Where(e => !updatedEntity.Est_Liers
                    .Any(up => up.Id_Thematique == e.Id_Thematique))
                .ToList();

            foreach (var estlier in toRemoveEstLier)
            {
                entityToUpdate.Est_Liers.Remove(estlier);
                _context.Est_Liers.Remove(estlier);
            }

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

        public async Task<IEnumerable<Laboratoire>> SearchAsync(Expression<Func<Laboratoire, bool>> predicate)
        {
            return await _context.Laboratoires
                 .Include(d => d.Designers)
                    .ThenInclude(m => m.Mot_ClefNavigation)
                 .Include(el => el.Est_Liers)
                    .ThenInclude(t => t.ThematiqueNavigation)
                .Include(a => a.Adresse_laboNavigation)
                .Include(c => c.Adresse_campusNavigation)
                .Where(predicate)
                .ToListAsync();
        }

    }
}
