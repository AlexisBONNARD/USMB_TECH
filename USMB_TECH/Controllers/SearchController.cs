﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Python.Runtime;
using USMB_TECH.DTO;
using USMB_TECH.Models.Repository;
using USMB_TECH_Blazor.Models;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
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

        // Détection des types activés
        bool useMotClef = mode.Contains("motclef") || mode == "global" || mode == "full";
        bool useThematique = mode.Contains("thematique") || mode == "global" || mode == "full";
        bool useTexte = mode.Contains("texte") || mode == "full";
        if (!PythonEngine.IsInitialized)
        {
            Python.Runtime.Runtime.PythonDLL = @"C:\ProgramData\anaconda3\python311.dll";
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();  
        }
        Console.WriteLine("Engine initialized");

        using (Py.GIL())
        {
            Console.WriteLine("GIL acquis");
            dynamic script = Py.Import("IASearch"); 
            Console.WriteLine("Point d'arrêt");
            dynamic resultIA = script.search(query);
            using var firstItem = new PyList(resultIA)[0];
            using var tuple = new PyTuple(firstItem)!;
            string resultat = tuple[1].As<String>();

            query = resultat.ToLower().Trim();
        }
        Console.WriteLine("La query : " + query);
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
        ))
        .ToList();

        var laboratoireDtos = _mapper.Map<List<LaboratoirePreviewDTO>>(laboratoires);

        //            DOMAINES D’EXCELLENCE
        var domaines = (await _domaineManager.SearchAsync(d =>
            (
                useTexte &&
                (
                    (d.intitule_Domaine_Excellence ?? "").ToLower().Contains(query) ||
                    (d.Description_Domaine_Excellence ?? "").ToLower().Contains(query)
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
