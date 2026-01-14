using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Python.Runtime;
using USMB_TECH.DTO;
using USMB_TECH.Models.Repository;
using USMB_TECH_Blazor.Models;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly EquipementManager _equipManager;
    private readonly Pole_ExpertiseManager _poleManager;
    private readonly PrestationManager _prestationManager;
    private readonly LaboratoireManager _laboratoireManager;
    private readonly Domaine_ExcellenceManager _domaineManager;
    private readonly IMapper _mapper;

    public SearchController(
        EquipementManager equipManager,
        Pole_ExpertiseManager poleManager,
        PrestationManager prestationManager,
        LaboratoireManager laboratoireManager,
        Domaine_ExcellenceManager domaineManager,
        IMapper mapper)
    {
        _equipManager = equipManager;
        _poleManager = poleManager;
        _prestationManager = prestationManager;
        _laboratoireManager = laboratoireManager;
        _domaineManager = domaineManager;
        _mapper = mapper;
    }

    [HttpGet("global")]
    public async Task<ActionResult<GlobalSearchResultDTO>> GlobalSearch(
     [FromQuery] string query,
     [FromQuery] string mode = "motclef")
    {
        if (string.IsNullOrWhiteSpace(query))
            return Ok(new GlobalSearchResultDTO());

        query = query.ToLower().Trim();

        // Détection du mode IA
        bool useIA = mode.Contains("ia");

        // Si mode IA, on utilise uniquement l'IA pour transformer la query
        // Sinon, on utilise les 3 modes classiques
        bool useMotClef = !useIA && (mode.Contains("motclef") || mode == "global" || mode == "full");
        bool useThematique = !useIA && (mode.Contains("thematique") || mode == "global" || mode == "full");
        bool useTexte = !useIA && (mode.Contains("texte") || mode == "full");

        // Si mode IA est activé, on traite la query avec Python
        if (useIA)
        {
            // Plus besoin de vérifier IsInitialized ni de définir PythonDLL
            // car c'est fait au démarrage dans Program.cs
            try
            {
                using (Py.GIL())
                {
                    Console.WriteLine("GIL acquis");
                    dynamic script = Py.Import("IASearch");
                    Console.WriteLine("Script Python importé");
                    dynamic resultIA = script.search(query, 10);
                    string resultat = "";
                    foreach (var item in resultIA)
                    {
                        using var tuple = new PyTuple(item)!;
                        resultat += " " + tuple[1].As<String>();
                    }

                    query = resultat.ToLower().Trim();
                    Console.WriteLine("Query transformée par IA : " + query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel à l'IA Python : {ex.Message}");
                // En cas d'erreur, continuer avec la query originale
            }
        }

        //                  ÉQUIPEMENTS
        var equipements = (await _equipManager.SearchAsync(e =>
            (
                useMotClef &&
                e.Pole_ExpertiseNavigation != null &&
                e.Pole_ExpertiseNavigation.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            )
            ||
            (
                useThematique &&
                e.Exposers.Any(t =>
                    t.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query))
            )
            ||
            (
                useTexte &&
                (
                    (e.Nom_Equipement ?? "").ToLower().Contains(query) ||
                    (e.Description_Technique ?? "").ToLower().Contains(query)
                )
            )
            ||
            (
                useIA &&
                (
                    (e.Pole_ExpertiseNavigation != null &&
                     e.Pole_ExpertiseNavigation.Specifiers.Any(s =>
                         query.Contains(s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower()))) ||
                    e.Exposers.Any(t =>
                        query.Contains(t.ThematiqueNavigation.Nom_Thematique.ToLower())) ||
                    query.Contains((e.Nom_Equipement ?? "").ToLower()) ||
                    query.Contains((e.Description_Technique ?? "").ToLower())
                )
            )
        ))
        .GroupBy(e => e.Id_Equipement)
        .Select(g => g.First())
        .ToList();

        var equipementDtos = _mapper.Map<List<EquipementPreviewDTO>>(equipements);

        //                  POLES EXPERTISE
        var poles = (await _poleManager.SearchAsync(p =>
            (
                useMotClef &&
                p.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            )
            ||
            (
                useTexte &&
                (
                    (p.Nom_Pole_Expertise ?? "").ToLower().Contains(query) ||
                    (p.Description_Pole_Expertise ?? "").ToLower().Contains(query)
                )
            )
            ||
            (
                useIA &&
                (
                    p.Specifiers.Any(s =>
                        query.Contains(s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower())) ||
                    query.Contains((p.Nom_Pole_Expertise ?? "").ToLower()) ||
                    query.Contains((p.Description_Pole_Expertise ?? "").ToLower())
                )
            )
        ))
        .ToList();

        var poleDtos = _mapper.Map<List<PoleExpertisePreviewDTO>>(poles);

        //                 PRESTATIONS
        var prestations = (await _prestationManager.SearchAsync(pr =>
            (
                useMotClef &&
                pr.Precisers.Any(p =>
                    p.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            )
            ||
            (
                useTexte &&
                (
                    (pr.Intitule_Prestation ?? "").ToLower().Contains(query) ||
                    (pr.Description_Prestation ?? "").ToLower().Contains(query)
                )
            )
            ||
            (
                useIA &&
                (
                    pr.Precisers.Any(p =>
                        query.Contains(p.Mot_ClefNavigation.Nom_Mot_Clef.ToLower())) ||
                    query.Contains((pr.Intitule_Prestation ?? "").ToLower()) ||
                    query.Contains((pr.Description_Prestation ?? "").ToLower())
                )
            )
        ))
        .ToList();

        var prestationDtos = _mapper.Map<List<PrestationPreviewDTO>>(prestations);

        //                LABORATOIRES
        var laboratoires = (await _laboratoireManager.SearchAsync(l =>
            (
                useMotClef &&
                l.Designers.Any(q =>
                    q.Mot_ClefNavigation != null &&
                    q.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            )
            ||
            (
                useThematique &&
                l.Est_Liers.Any(t =>
                    t.ThematiqueNavigation != null &&
                    t.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query))
            )
            ||
            (
                useTexte &&
                (
                    (l.Nom_Long ?? "").ToLower().Contains(query) ||
                    (l.Description ?? "").ToLower().Contains(query) ||
                    (
                        l.Adresse_laboNavigation != null &&
                        (
                            (l.Adresse_laboNavigation.Ville_Adresse ?? "").ToLower().Contains(query) ||
                            (l.Adresse_laboNavigation.Pays_Adresse ?? "").ToLower().Contains(query)
                        )
                    )
                )
            )
            ||
            (
                useIA &&
                (
                    l.Designers.Any(q =>
                        q.Mot_ClefNavigation != null &&
                        query.Contains(q.Mot_ClefNavigation.Nom_Mot_Clef.ToLower())) ||
                    l.Est_Liers.Any(t =>
                        t.ThematiqueNavigation != null &&
                        query.Contains(t.ThematiqueNavigation.Nom_Thematique.ToLower())) ||
                    query.Contains((l.Nom_Long ?? "").ToLower()) ||
                    query.Contains((l.Description ?? "").ToLower()) ||
                    (
                        l.Adresse_laboNavigation != null &&
                        (
                            query.Contains((l.Adresse_laboNavigation.Ville_Adresse ?? "").ToLower()) ||
                            query.Contains((l.Adresse_laboNavigation.Pays_Adresse ?? "").ToLower())
                        )
                    )
                )
            )
        ))
        .ToList();

        var laboratoireDtos = _mapper.Map<List<LaboratoirePreviewDTO>>(laboratoires);

        //            DOMAINES D'EXCELLENCE
        var domaines = (await _domaineManager.SearchAsync(d =>
            (
                useTexte &&
                (
                    (d.intitule_Domaine_Excellence ?? "").ToLower().Contains(query) ||
                    (d.Description_Domaine_Excellence ?? "").ToLower().Contains(query)
                )
            )
            ||
            (
                useIA &&
                (
                    query.Contains((d.intitule_Domaine_Excellence ?? "").ToLower()) ||
                    query.Contains((d.Description_Domaine_Excellence ?? "").ToLower())
                )
            )
        ))
        .ToList();

        var domaineDtos = _mapper.Map<List<DomaineExcellenceDTO>>(domaines);

        //                 RÉSULTAT GLOBAL
        var result = new GlobalSearchResultDTO
        {
            Equipements = equipementDtos,
            PolesExpertise = poleDtos,
            Prestations = prestationDtos,
            Laboratoires = laboratoireDtos,
            DomainesExcellence = domaineDtos
        };

        return Ok(result);
    }
}