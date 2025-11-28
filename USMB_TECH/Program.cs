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
builder.Services.AddScoped<IMainRepository<Plateforme, int>, PlateformeManager>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/") });
builder.Services.AddScoped<IMainRepository<Thematique, int>, ThematiqueManager>();
builder.Services.AddScoped<IMainRepository<Prestation, int>, PrestationManager>();
builder.Services.AddScoped<IMainRepository<Contact_USMB, int>, Contact_USMBManager>();
builder.Services.AddScoped<IMainRepository<Equipement, int>, EquipementManager>();
builder.Services.AddScoped<IMainRepository<Type_Equipement, int>, Type_EquipementManager>();
builder.Services.AddScoped<IMainRepository<Marque, int>, MarqueManager>();

builder.Services.AddScoped<EquipementManager>();
builder.Services.AddScoped<LaboratoireManager>();
builder.Services.AddScoped<PlateformeManager>();
builder.Services.AddScoped<PrestationManager>();
builder.Services.AddScoped<ThematiqueManager>();
builder.Services.AddScoped<MarqueManager>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<UsmbTechDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("UsmbTechDbContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("blazor-usmbtech-ekf6gkgretedd7bc.francecentral-01.azurewebsites.net")
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

app.Run();
