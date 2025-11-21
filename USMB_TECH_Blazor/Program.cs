using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using USMB_TECH_Blazor;
using USMB_TECH_Blazor.Components;
using USMB_TECH_Blazor.Models;
using USMB_TECH_Blazor.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMainService<Equipement, int>>(eq => new WebService<Equipement, int>("Equipements"));
builder.Services.AddScoped<IMainService<Laboratoire, string>>(eq => new WebService<Laboratoire, string>("Laboratoires"));
builder.Services.AddScoped<IMainService<Plateforme, int>>(eq => new WebService<Plateforme, int>("Plateformes"));
builder.Services.AddScoped<IMainService<Prestation, int>>(eq => new WebService<Prestation, int>("Prestations"));
builder.Services.AddScoped<IMainService<Thematique, int>>(eq => new WebService<Thematique, int>("Thematiques"));
builder.Services.AddScoped<IMainService<Type_Equipement, int>>(eq => new WebService<Type_Equipement, int>("Type_Equipements"));
builder.Services.AddScoped<IMainService<Marque, int>>(eq => new WebService<Marque, int>("Marques")); 

//builder.Services.AddHttpClient(); // Assurez-vous que HttpClient est disponible
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7093/") // URL de ton API
});
builder.Services.AddScoped<SearchService>();
await builder.Build().RunAsync();
