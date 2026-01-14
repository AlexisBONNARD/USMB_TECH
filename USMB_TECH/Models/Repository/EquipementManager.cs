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
                .Include(e => e.Pole_ExpertiseNavigation)
                    .ThenInclude(p => p.Domaine_ExcellenceNavigation)
                .Include(e => e.ModeleNavigation)
                    .ThenInclude(m => m.MarqueNavigation)
                .Include(e => e.Type_EquipementNavigation)
                .Include(e => e.Consommers)
                .Include(e => e.Posseders)
                    .ThenInclude(f => f.FonctionnaliteNavigation)
                .Include(e => e.Exemple_Utilisations)
                .Include(e => e.Referencers)
                .Include(e => e.Prise_Contacts)
                .Include(e => e.Photos)
                .Include(e => e.Qualifiers)
                    .ThenInclude(q => q.Mot_ClefNavigation)
                .Include(e => e.Fournirs)
                    .ThenInclude(f => f.PrestationNavigation)
                        .ThenInclude(p => p.Type_PrestationNavigation)
                .Include(e => e.Exposers)
                    .ThenInclude(ex => ex.ThematiqueNavigation)
                .FirstOrDefaultAsync(e => e.Id_Equipement == id);
        }


        public async Task AddAsync(Equipement entity)
        {
            // -------------------
            // 1️⃣ Gestion Pole_Expertise
            // -------------------
            if (entity.Pole_ExpertiseNavigation != null &&
                !string.IsNullOrEmpty(entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise))
            {
                var pole = await _context.Pole_Expertises
                    .FirstOrDefaultAsync(p => p.Nom_Pole_Expertise == entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise);

                if (pole == null)
                {
                    pole = new Pole_Expertise
                    {
                        Nom_Pole_Expertise = entity.Pole_ExpertiseNavigation.Nom_Pole_Expertise
                    };
                    _context.Pole_Expertises.Add(pole);
                    await _context.SaveChangesAsync();
                }

                entity.Id_Pole_Expertise = pole.Id_Pole_Expertise;
                entity.Pole_ExpertiseNavigation = pole;
            }

            // -------------------
            // 2️⃣ Gestion Type_Equipement
            // -------------------
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

            // -------------------
            // 3️⃣ Gestion Modele + Marque
            // -------------------
            if (entity.ModeleNavigation != null)
            {
                // Marque
                if (entity.ModeleNavigation.MarqueNavigation != null &&
                    !string.IsNullOrWhiteSpace(entity.ModeleNavigation.MarqueNavigation.Nom_Marque))
                {
                    var marqueName = entity.ModeleNavigation.MarqueNavigation.Nom_Marque;
                    var existingMarque = await _context.Marques
                        .FirstOrDefaultAsync(m => m.Nom_Marque == marqueName);

                    if (existingMarque == null)
                    {
                        existingMarque = new Marque { Nom_Marque = marqueName };
                        _context.Marques.Add(existingMarque);
                        await _context.SaveChangesAsync();
                    }

                    entity.ModeleNavigation.Id_Marque = existingMarque.Id_Marque;
                    entity.ModeleNavigation.MarqueNavigation = existingMarque;
                }

                // Modele
                if (!string.IsNullOrWhiteSpace(entity.ModeleNavigation.Nom_Modele))
                {
                    var existingModele = await _context.Modeles
                        .FirstOrDefaultAsync(m => m.Nom_Modele == entity.ModeleNavigation.Nom_Modele);

                    if (existingModele == null)
                    {
                        existingModele = new Modele
                        {
                            Nom_Modele = entity.ModeleNavigation.Nom_Modele,
                            Id_Marque = entity.ModeleNavigation.Id_Marque,
                            MarqueNavigation = entity.ModeleNavigation.MarqueNavigation
                        };
                        _context.Modeles.Add(existingModele);
                        await _context.SaveChangesAsync();
                    }

                    entity.Id_Modele = existingModele.Id_Modele;
                    entity.ModeleNavigation = existingModele;
                }
            }

            // -------------------
            // 4️⃣ Gestion Qualifiers et MotsClés
            // -------------------
            if (entity.Qualifiers != null && entity.Qualifiers.Any())
            {
                var qualifierFinal = new List<Qualifier>();

                foreach (var qualifier in entity.Qualifiers)
                {
                    if (qualifier.Mot_ClefNavigation == null ||
                        string.IsNullOrWhiteSpace(qualifier.Mot_ClefNavigation.Nom_Mot_Clef))
                        continue;

                    var motClef = await _context.Mot_Clefs
                        .FirstOrDefaultAsync(mc => mc.Nom_Mot_Clef == qualifier.Mot_ClefNavigation.Nom_Mot_Clef);

                    if (motClef == null)
                    {
                        motClef = new Mot_Clef
                        {
                            Nom_Mot_Clef = qualifier.Mot_ClefNavigation.Nom_Mot_Clef
                        };
                        _context.Mot_Clefs.Add(motClef);
                        await _context.SaveChangesAsync();
                    }

                    qualifierFinal.Add(new Qualifier
                    {
                        Mot_ClefNavigation = motClef
                    });
                }

                entity.Qualifiers = qualifierFinal;
            }

            // -------------------
            // 5️⃣ Ajouter Equipement (sans Posseders)
            // -------------------
            var possedersTemp = entity.Posseders; // sauvegarde temporaire
            entity.Posseders = new List<Posseder>(); // vide pour l'instant
            _context.Equipements.Add(entity);
            await _context.SaveChangesAsync(); // Id_Equipement généré

            // -------------------
            // 6️⃣ Gestion Posseders (Fonctionnalites)
            // -------------------
            if (possedersTemp != null && possedersTemp.Any())
            {
                var possedersFinal = new List<Posseder>();

                foreach (var posseder in possedersTemp)
                {
                    if (posseder?.FonctionnaliteNavigation == null ||
                        string.IsNullOrWhiteSpace(posseder.FonctionnaliteNavigation.Nom_Fonctionnalite))
                        continue;

                    var fonctionnalite = await _context.Fonctionnalites
                        .FirstOrDefaultAsync(f => f.Nom_Fonctionnalite == posseder.FonctionnaliteNavigation.Nom_Fonctionnalite);

                    if (fonctionnalite == null)
                    {
                        fonctionnalite = new Fonctionnalite
                        {
                            Nom_Fonctionnalite = posseder.FonctionnaliteNavigation.Nom_Fonctionnalite,
                            Description = posseder.FonctionnaliteNavigation.Description
                        };
                        _context.Fonctionnalites.Add(fonctionnalite);
                        await _context.SaveChangesAsync(); // Id_Fonctionnalite généré
                    }

                    possedersFinal.Add(new Posseder
                    {
                        Id_Equipement = entity.Id_Equipement,
                        FonctionnaliteNavigation = fonctionnalite
                    });
                }

                _context.Posseders.AddRange(possedersFinal);
                await _context.SaveChangesAsync();
            }
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