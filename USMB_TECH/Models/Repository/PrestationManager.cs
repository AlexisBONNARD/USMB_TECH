using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using USMB_TECH.DTO;
using USMB_TECH.Models.EntityFramework;

namespace USMB_TECH.Models.Repository
{
    public class PrestationManager : IMainRepository<Prestation, int>
    {
        private readonly UsmbTechDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public PrestationManager(UsmbTechDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prestation>> GetAllAsync()
        {
            return await _context.Prestations.ToListAsync();
        }

        public async Task<Prestation?> GetByIdAsync(int id)
        {
            return await _context.Prestations
                .Include(p => p.Photos)    
                .Include(p => p.Presenters)
                    .ThenInclude(e => e.Pole_ExpertiseNavigation)

                .Include(p => p.Fournirs)
                    .ThenInclude(e => e.EquipementNavigation)
         
                .FirstOrDefaultAsync(p => p.Id_Prestation == id);
        }

        public async Task AddAsync(Prestation entity)
        {
            if (entity.Contact_USMBNavigation != null && !string.IsNullOrEmpty(entity.Contact_USMBNavigation.Nom_Contact))
            {
                var contact = await _context.Contact_USMBs
                    .FirstOrDefaultAsync(c => c.Nom_Contact == entity.Contact_USMBNavigation.Nom_Contact);

                if (contact == null)
                {
                    contact = new Contact_USMB
                    {
                        Nom_Contact = entity.Contact_USMBNavigation.Nom_Contact
                    };
                    _context.Contact_USMBs.Add(contact);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Contact = contact.Id_Contact;
                entity.Contact_USMBNavigation = contact;
            }
            if(entity.Unite_OeuvreNavigation != null && !string.IsNullOrEmpty(entity.Unite_OeuvreNavigation.Nom_Unite_Oeuvre)) 
            {
                var unite = await _context.Unite_Oeuvres.FirstOrDefaultAsync(u => u.Nom_Unite_Oeuvre == entity.Unite_OeuvreNavigation.Nom_Unite_Oeuvre);
                entity.Id_Unite_Oeuvre = unite.Id_Unite_Oeuvre;
                entity.Unite_OeuvreNavigation = unite;
            }
            if(entity.Type_PrestationNavigation != null && !string.IsNullOrEmpty(entity.Type_PrestationNavigation.Nom_Type_Prestation)) 
            {
                var type = await _context.Type_Prestations.FirstOrDefaultAsync(tp => tp.Nom_Type_Prestation == entity.Type_PrestationNavigation.Nom_Type_Prestation);

                if(type == null) 
                {
                    type = new Type_Prestation
                    {
                        Nom_Type_Prestation = entity.Type_PrestationNavigation.Nom_Type_Prestation
                    };
                    _context.Type_Prestations.Add(type);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Type_Prestation = type.Id_Type_Prestation;
                entity.Type_PrestationNavigation = type;
            }
            if(entity.Domaine_ExcellenceNavigation != null && !string.IsNullOrEmpty( entity.Domaine_ExcellenceNavigation.intitule_Domaine_Excellence))
            {
                var domaine = await _context.Domaine_Excellences
                    .FirstOrDefaultAsync(d => d.intitule_Domaine_Excellence == entity.Domaine_ExcellenceNavigation.intitule_Domaine_Excellence);
                if (domaine == null)
                {
                    domaine = new Domaine_Excellence
                    {
                        intitule_Domaine_Excellence = entity.Domaine_ExcellenceNavigation.intitule_Domaine_Excellence
                    };
                    _context.Domaine_Excellences.Add(domaine);
                    await _context.SaveChangesAsync();
                }
                entity.Id_Domaine_Excellence = domaine.Id_Domaine_Excellence;
                entity.Domaine_ExcellenceNavigation = domaine;
            }

            if(entity.Precisers != null && entity.Precisers.Any())
            {
                var preciserFinal = new List<Preciser>();
                foreach(var preciser in entity.Precisers) 
                {
                    var motClef = await _context.Mot_Clefs.FirstOrDefaultAsync(mc => mc.Nom_Mot_Clef == preciser.Mot_ClefNavigation.Nom_Mot_Clef);
                    if (motClef == null)
                    {
                        motClef = preciser.Mot_ClefNavigation;
                        _context.Mot_Clefs.Add(motClef);
                    }
                    preciserFinal.Add(new Preciser
                    {
                        Mot_ClefNavigation = motClef,
                        Id_Prestation = entity.Id_Prestation
                    });
                }
                entity.Precisers = preciserFinal;
            }

            if(entity.Presenters != null && entity.Presenters.Any()) 
            {
                var presenterFinal = new List<Presenter>();
                foreach(var presenter in entity.Presenters) 
                {
                    var pole = await _context.Pole_Expertises.FirstOrDefaultAsync(pe => pe.Nom_Pole_Expertise == presenter.Pole_ExpertiseNavigation.Nom_Pole_Expertise);
                    if(pole == null) 
                    {
                        pole = presenter.Pole_ExpertiseNavigation;
                        _context.Pole_Expertises.Add(pole);
                   
                    }
                    presenterFinal.Add(new Presenter
                    {
                        Pole_ExpertiseNavigation = pole,
                        Id_Prestation = entity.Id_Prestation
                    });
                }
                entity.Presenters = presenterFinal;
            }

            if(entity.LaboratoireNavigation != null && !string.IsNullOrEmpty(entity.LaboratoireNavigation.Nom_Court)) 
            {
               var labo = await _context.Laboratoires.FirstOrDefaultAsync(l => l.Nom_Court == entity.LaboratoireNavigation.Nom_Court);
                if(labo is null) 
                {
                    labo = new Laboratoire
                    {
                        Nom_Court = entity.LaboratoireNavigation.Nom_Court
                    };
                    _context.Laboratoires.Add(labo);
                    await _context.SaveChangesAsync();
                }
                entity.Nom_Court = labo.Nom_Court;
                entity.LaboratoireNavigation = labo;

            }
            _context.Prestations.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Prestation entityToUpdate, Prestation updatedEntity)
        {
            // -------------------------
            // 1️ Mettre à jour les propriétés simples
            // -------------------------
            _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

            // -------------------------
            // 2️ Mise à jour du contact (si navigation 1-1)
            // -------------------------
            if (updatedEntity.Contact_USMBNavigation != null)
            {
                entityToUpdate.Contact_USMBNavigation = updatedEntity.Contact_USMBNavigation;
            }

            // -------------------------
            // 3️ Gestion incrémentale des mots-clés via Preciser
            // -------------------------
            updatedEntity.Precisers ??= new List<Preciser>();

            foreach (var updatedPreciser in updatedEntity.Precisers)
            {
                var existingPreciser = entityToUpdate.Precisers
                    .FirstOrDefault(p => p.Id_Mot_Clef == updatedPreciser.Id_Mot_Clef);

                if (existingPreciser == null)
                {
                    updatedPreciser.Id_Prestation = entityToUpdate.Id_Prestation;
                    updatedPreciser.PrestationNavigation = entityToUpdate;
                    entityToUpdate.Precisers.Add(updatedPreciser);
                }
            }

            var precisersToRemove = entityToUpdate.Precisers
                .Where(p => !updatedEntity.Precisers.Any(up => up.Id_Mot_Clef == p.Id_Mot_Clef))
                .ToList();

            foreach (var preciser in precisersToRemove)
            {
                entityToUpdate.Precisers.Remove(preciser);
                _context.Precisers.Remove(preciser);
            }

            // -------------------------
            // 4️ Sauvegarde
            // -------------------------
            await _context.SaveChangesAsync();
        }




        public async Task DeleteAsync(Prestation entity)
        {
            _context.Prestations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prestation>> GetByKeysAsync<TProperty>(
    Expression<Func<Prestation, TProperty>> propertySelector,
    TProperty value)
        {
            return await _context.Prestations
                .Where(p => EF.Property<TProperty>(p, ((MemberExpression)propertySelector.Body).Member.Name).Equals(value))
                .ToListAsync();
        }

        public async Task<IEnumerable<Prestation>> SearchAsync(Expression<Func<Prestation, bool>> predicate)
        {
            return await _context.Prestations
                .Include(P => P.Precisers)
                    .ThenInclude(MC => MC.Mot_ClefNavigation)
                .Where(predicate)
                .ToListAsync();
        }

    }
}
