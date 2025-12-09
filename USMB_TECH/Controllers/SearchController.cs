using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        // 🔍 Équipements
        var equipements = (await _equipManager.SearchAsync(e =>
            ((mode.Contains("motclef") || mode == "global") &&
                e.Pole_ExpertiseNavigation != null &&
                e.Pole_ExpertiseNavigation.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query)))
            ||
            ((mode.Contains("thematique") || mode == "global") &&
                e.Exposers.Any(t =>
                    t.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query)))
            ||
            (mode.Contains("texte") &&
                ((e.Nom_Equipement ?? "").ToLower().Contains(query) ||
                 (e.Description_Technique ?? "").ToLower().Contains(query)))
        ))
        .GroupBy(e => e.Id_Equipement)
        .Select(g => g.First())
        .ToList();

        var equipementDtos = _mapper.Map<List<EquipementPreviewDTO>>(equipements);

        // 🔍 Pôles Expertise
        var poles = (await _poleManager.SearchAsync(p =>
            (mode.Contains("motclef") || mode == "global") &&
                p.Specifiers.Any(s =>
                    s.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            ||
            (mode.Contains("texte") &&
                ((p.Nom_Pole_Expertise ?? "").ToLower().Contains(query) ||
                 (p.Description_Pole_Expertise ?? "").ToLower().Contains(query)))
        ))
        .ToList();

        var poleDtos = _mapper.Map<List<PoleExpertisePreviewDTO>>(poles);

        // 🔍 Prestations
        var prestations = (await _prestationManager.SearchAsync(pr =>
            (mode.Contains("motclef") || mode == "global") &&
                pr.Precisers.Any(p =>
                    p.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query))
            ||
            (mode.Contains("texte") &&
                ((pr.Intitule_Prestation ?? "").ToLower().Contains(query) ||
                 (pr.Description_Prestation ?? "").ToLower().Contains(query)))
        ))
        .ToList();

        var prestationDtos = _mapper.Map<List<PrestationPreviewDTO>>(prestations);

        // 🔍 Laboratoires
        var laboratoires = (await _laboratoireManager.SearchAsync(l =>
            // 🔍 motclef OU global
            ((mode.Contains("motclef") || mode == "global") &&
                l.Designers.Any(q =>
                    q.Mot_ClefNavigation != null &&
                    q.Mot_ClefNavigation.Nom_Mot_Clef.ToLower().Contains(query)))
            ||
            // 🔍 thematique OU global
            ((mode.Contains("thematique") || mode == "global") &&
                l.Est_Liers.Any(t =>
                    t.ThematiqueNavigation != null &&
                    t.ThematiqueNavigation.Nom_Thematique.ToLower().Contains(query)))
            ||
            // 🔍 texte uniquement si mode=texte
            (mode.Contains("texte") &&
                ((l.Nom_Long ?? "").ToLower().Contains(query) ||
                 (l.Description ?? "").ToLower().Contains(query) ||
                 (l.Adresse_laboNavigation != null &&
                     ((l.Adresse_laboNavigation.Ville_Adresse ?? "").ToLower().Contains(query) ||
                      (l.Adresse_laboNavigation.Pays_Adresse ?? "").ToLower().Contains(query)))))
        ))
        .ToList();

        var laboratoireDtos = _mapper.Map<List<LaboratoirePreviewDTO>>(laboratoires);

        // 🔍 Domaines d’Excellence
        var domaines = (await _domaineManager.SearchAsync(d =>
            (mode.Contains("texte") || mode == "global") &&
                ((d.intitule_Domaine_Excellence ?? "").ToLower().Contains(query) ||
                 (d.Description_Domaine_Excellence ?? "").ToLower().Contains(query))
        ))
        .ToList();

        var domaineDtos = _mapper.Map<List<DomaineExcellenceDTO>>(domaines);

        // 🧩 Construction du DTO global
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
