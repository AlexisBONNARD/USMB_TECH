using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMainRepository<Laboratoire, string>, LaboratoireManager>();
builder.Services.AddScoped<IMainRepository<Pole_Expertise, int>, Pole_ExpertiseManager>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://blazor-usmbtech-ekf6gkgretedd7bc.francecentral-01.azurewebsites.net/") });
builder.Services.AddScoped<IMainRepository<Thematique, int>, ThematiqueManager>();
builder.Services.AddScoped<IMainRepository<Prise_Contact, int>, Prise_ContactManager>();
builder.Services.AddScoped<IMainRepository<Prestation, int>, PrestationManager>();
builder.Services.AddScoped<IMainRepository<Contact_USMB, int>, Contact_USMBManager>();
builder.Services.AddScoped<IMainRepository<Equipement, int>, EquipementManager>();
builder.Services.AddScoped<IMainRepository<Type_Equipement, int>, Type_EquipementManager>();
builder.Services.AddScoped<IMainRepository<Marque, int>, MarqueManager>();
builder.Services.AddScoped<IMainRepository<Domaine_Excellence, int>, Domaine_ExcellenceManager>();
builder.Services.AddScoped<IMainRepository<Mot_Clef, int>, MotClefManager>();
builder.Services.AddScoped<IMainRepository<Fonction, int>, FonctionManager>();
builder.Services.AddScoped<IMainRepository<Type_Prestation, int>, Type_PrestationManager>();
builder.Services.AddScoped<IMainRepository<Unite_Oeuvre, int>, Unite_OeuvreManager>();

builder.Services.AddScoped<EquipementManager>();
builder.Services.AddScoped<LaboratoireManager>();
builder.Services.AddScoped<Pole_ExpertiseManager>();
builder.Services.AddScoped<PrestationManager>();
builder.Services.AddScoped<ThematiqueManager>();
builder.Services.AddScoped<Prise_ContactManager>();
builder.Services.AddScoped<MarqueManager>();
builder.Services.AddScoped<MotClefManager>();
builder.Services.AddScoped<Domaine_ExcellenceManager>();    

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<UsmbTechDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("UsmbTechDbContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("https://blazor-usmbtech-ekf6gkgretedd7bc.francecentral-01.azurewebsites.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

// IMPORTANT : ordre correct
app.UseDefaultFiles();
app.UseStaticFiles();

// Map API controllers
app.MapControllers();

// LAST : Blazor fallback
app.MapFallbackToFile("index.html");

app.MapGet("/api/geocode", async (string street, string city, string country, string postalcode) =>
{
    using var client = new HttpClient();
    client.DefaultRequestHeaders.UserAgent.ParseAdd("USMB-TECH/1.0");

    var queryParams = new Dictionary<string, string>
    {
        ["street"] = street,
        ["city"] = city,
        ["country"] = country,
        ["postalcode"] = postalcode,
        ["format"] = "json",
        ["limit"] = "1"
    };

    var url = QueryHelpers.AddQueryString("https://nominatim.openstreetmap.org/search", queryParams);
    var json = await client.GetStringAsync(url);

    return Results.Content(json, "application/json");
});

app.Run();
