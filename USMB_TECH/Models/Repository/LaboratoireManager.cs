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
            // -------------------------
            // 1. Champs simples
            // -------------------------
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            // -------------------------
            // 2. Adresse labo
            // -------------------------
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
                    existingLabo = updatedEntity.Adresse_laboNavigation;
                    _context.Adresses.Add(existingLabo);
                    await _context.SaveChangesAsync();
                }

                entityToUpdate.Id_Adresse_Labo = existingLabo.Id_Adresse;
                entityToUpdate.Adresse_laboNavigation = existingLabo;
            }

            // -------------------------
            // 3. Adresse campus
            // -------------------------
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
                    existingCampus = updatedEntity.Adresse_campusNavigation;
                    _context.Adresses.Add(existingCampus);
                    await _context.SaveChangesAsync();
                }

                entityToUpdate.Id_Adresse_Campus = existingCampus.Id_Adresse;
                entityToUpdate.Adresse_campusNavigation = existingCampus;
            }

            // -------------------------
            // 4. Gerers (pôles d’expertise)
            // -------------------------
            updatedEntity.Gerers ??= new List<Gerer>();

            var toRemoveGerer = entityToUpdate.Gerers
                .Where(g => !updatedEntity.Gerers.Any(up => up.Id_Pole_Expertise == g.Id_Pole_Expertise))
                .ToList();

            foreach (var g in toRemoveGerer)
            {
                entityToUpdate.Gerers.Remove(g);
                _context.Gerers.Remove(g);
            }

            foreach (var g in updatedEntity.Gerers)
            {
                if (!entityToUpdate.Gerers.Any(x => x.Id_Pole_Expertise == g.Id_Pole_Expertise))
                {
                    g.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Gerers.Add(g);
                }
            }

            // -------------------------
            // 5. Designers (mots‑clés)
            // -------------------------
            updatedEntity.Designers ??= new List<Designer>();

            var toRemoveDesigner = entityToUpdate.Designers
                .Where(d => !updatedEntity.Designers.Any(up => up.Id_Mot_Clef == d.Id_Mot_Clef))
                .ToList();

            foreach (var d in toRemoveDesigner)
            {
                entityToUpdate.Designers.Remove(d);
                _context.Designers.Remove(d);
            }

            foreach (var d in updatedEntity.Designers)
            {
                if (!entityToUpdate.Designers.Any(x => x.Id_Mot_Clef == d.Id_Mot_Clef))
                {
                    d.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Designers.Add(d);
                }
            }

            // -------------------------
            // 6. Est_Liers (thématiques)
            // -------------------------
            updatedEntity.Est_Liers ??= new List<Est_Lier>();

            var toRemoveEstLier = entityToUpdate.Est_Liers
                .Where(e => !updatedEntity.Est_Liers.Any(up => up.Id_Thematique == e.Id_Thematique))
                .ToList();

            foreach (var e in toRemoveEstLier)
            {
                entityToUpdate.Est_Liers.Remove(e);
                _context.Est_Liers.Remove(e);
            }

            foreach (var e in updatedEntity.Est_Liers)
            {
                if (!entityToUpdate.Est_Liers.Any(x => x.Id_Thematique == e.Id_Thematique))
                {
                    e.Nom_Court = entityToUpdate.Nom_Court;
                    entityToUpdate.Est_Liers.Add(e);
                }
            }

            // -------------------------
            // 7. Save final
            // -------------------------
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
