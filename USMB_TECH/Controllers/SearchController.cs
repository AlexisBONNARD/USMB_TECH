using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.DTO;
using USMB_TECH.Models.Repository;
using USMB_TECH_Blazor.Models;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private const string COLLATION = "French_CI_AI";

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
        [FromQuery] string mode = "full")
    {
        if (string.IsNullOrWhiteSpace(query))
            return Ok(new GlobalSearchResultDTO());

        query = query.Trim();
        var like = $"%{query}%";

        bool useMotClef = mode.Contains("motclef") || mode == "global" || mode == "full";
        bool useThematique = mode.Contains("thematique") || mode == "global" || mode == "full";
        bool useTexte = mode.Contains("texte") || mode == "full";

        // ================= ÉQUIPEMENTS =================
        var equipements = (await _equipManager.SearchAsync(e =>
            (useMotClef &&
                e.Pole_ExpertiseNavigation != null &&
                e.Pole_ExpertiseNavigation.Specifiers.Any(s =>
                    EF.Functions.Like(
                        EF.Functions.Collate(s.Mot_ClefNavigation.Nom_Mot_Clef, COLLATION),
                        like)))
            ||
            (useThematique &&
                e.Exposers.Any(t =>
                    EF.Functions.Like(
                        EF.Functions.Collate(t.ThematiqueNavigation.Nom_Thematique, COLLATION),
                        like)))
            ||
            (useTexte &&
                (
                    EF.Functions.Like(EF.Functions.Collate(e.Nom_Equipement ?? "", COLLATION), like) ||
                    EF.Functions.Like(EF.Functions.Collate(e.Description_Technique ?? "", COLLATION), like)
                ))
        ))
        .GroupBy(e => e.Id_Equipement)
        .Select(g => g.First())
        .ToList();

        // ================= POLES =================
        var poles = (await _poleManager.SearchAsync(p =>
            (useMotClef &&
                p.Specifiers.Any(s =>
                    EF.Functions.Like(
                        EF.Functions.Collate(s.Mot_ClefNavigation.Nom_Mot_Clef, COLLATION),
                        like)))
            ||
            (useTexte &&
                (
                    EF.Functions.Like(EF.Functions.Collate(p.Nom_Pole_Expertise ?? "", COLLATION), like) ||
                    EF.Functions.Like(EF.Functions.Collate(p.Description_Pole_Expertise ?? "", COLLATION), like)
                ))
        )).ToList();

        // ================= PRESTATIONS =================
        var prestations = (await _prestationManager.SearchAsync(pr =>
            (useMotClef &&
                pr.Precisers.Any(p =>
                    EF.Functions.Like(
                        EF.Functions.Collate(p.Mot_ClefNavigation.Nom_Mot_Clef, COLLATION),
                        like)))
            ||
            (useTexte &&
                (
                    EF.Functions.Like(EF.Functions.Collate(pr.Intitule_Prestation ?? "", COLLATION), like) ||
                    EF.Functions.Like(EF.Functions.Collate(pr.Description_Prestation ?? "", COLLATION), like)
                ))
        )).ToList();

        // ================= LABORATOIRES =================
        var laboratoires = (await _laboratoireManager.SearchAsync(l =>
            (useMotClef &&
                l.Designers.Any(d =>
                    d.Mot_ClefNavigation != null &&
                    EF.Functions.Like(
                        EF.Functions.Collate(d.Mot_ClefNavigation.Nom_Mot_Clef, COLLATION),
                        like)))
            ||
            (useThematique &&
                l.Est_Liers.Any(t =>
                    t.ThematiqueNavigation != null &&
                    EF.Functions.Like(
                        EF.Functions.Collate(t.ThematiqueNavigation.Nom_Thematique, COLLATION),
                        like)))
            ||
            (useTexte &&
                (
                    EF.Functions.Like(EF.Functions.Collate(l.Nom_Long ?? "", COLLATION), like) ||
                    EF.Functions.Like(EF.Functions.Collate(l.Description ?? "", COLLATION), like) ||
                    (
                        l.Adresse_laboNavigation != null &&
                        (
                            EF.Functions.Like(EF.Functions.Collate(l.Adresse_laboNavigation.Ville_Adresse ?? "", COLLATION), like) ||
                            EF.Functions.Like(EF.Functions.Collate(l.Adresse_laboNavigation.Pays_Adresse ?? "", COLLATION), like)
                        )
                    )
                ))
        )).ToList();

        // ================= DOMAINES =================
        var domaines = (await _domaineManager.SearchAsync(d =>
            useTexte &&
            (
                EF.Functions.Like(EF.Functions.Collate(d.intitule_Domaine_Excellence ?? "", COLLATION), like) ||
                EF.Functions.Like(EF.Functions.Collate(d.Description_Domaine_Excellence ?? "", COLLATION), like)
            )
        )).ToList();

        return Ok(new GlobalSearchResultDTO
        {
            Equipements = _mapper.Map<List<EquipementPreviewDTO>>(equipements),
            PolesExpertise = _mapper.Map<List<PoleExpertisePreviewDTO>>(poles),
            Prestations = _mapper.Map<List<PrestationPreviewDTO>>(prestations),
            Laboratoires = _mapper.Map<List<LaboratoirePreviewDTO>>(laboratoires),
            DomainesExcellence = _mapper.Map<List<DomaineExcellenceDTO>>(domaines)
        });
    }
}
