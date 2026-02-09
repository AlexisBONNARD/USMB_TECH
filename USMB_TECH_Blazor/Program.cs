using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using USMB_TECH.Models;
using USMB_TECH_Blazor;
using USMB_TECH_Blazor.Components;
using USMB_TECH_Blazor.Models;
using USMB_TECH_Blazor.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/")
});

builder.Services.AddScoped<AdminState>();

builder.Services.AddScoped<IMainService<Equipement, int>>(sp => new WebService<Equipement, int>(sp.GetRequiredService<HttpClient>(), "Equipements"));
builder.Services.AddScoped<IMainService<Laboratoire, string>>(sp => new WebService<Laboratoire, string>(sp.GetRequiredService<HttpClient>(), "Laboratoires"));
builder.Services.AddScoped<IMainService<LaboratoireUpdate, string>>(sp => new WebService<LaboratoireUpdate, string>(sp.GetRequiredService<HttpClient>(), "Laboratoires"));
builder.Services.AddScoped<IMainService<Pole_Expertise, int>>(sp => new WebService<Pole_Expertise, int>(sp.GetRequiredService<HttpClient>(), "Pole_Expertises"));
builder.Services.AddScoped<IMainService<Prise_Contact, int>>(sp => new WebService<Prise_Contact, int>(sp.GetRequiredService<HttpClient>(), "Prise_Contacts"));
builder.Services.AddScoped<IMainService<Prestation, int>>(sp => new WebService<Prestation, int>(sp.GetRequiredService<HttpClient>(), "Prestations"));
builder.Services.AddScoped<IMainService<PrestationUpdateModel, int>>(sp => new WebService<PrestationUpdateModel, int>(sp.GetRequiredService<HttpClient>(), "Prestations"));
builder.Services.AddScoped<IMainService<Thematique, int>>(sp => new WebService<Thematique, int>(sp.GetRequiredService<HttpClient>(), "thematique"));
builder.Services.AddScoped<IMainService<Type_Equipement, int>>(sp => new WebService<Type_Equipement, int>(sp.GetRequiredService<HttpClient>(), "Type_Equipements"));
builder.Services.AddScoped<IMainService<Type_Prestation, int>>(sp => new WebService<Type_Prestation, int>(sp.GetRequiredService<HttpClient>(), "Type_Prestations"));
builder.Services.AddScoped<IMainService<Type_Client, int>>(sp => new WebService<Type_Client, int>(sp.GetRequiredService<HttpClient>(), "type_client"));
builder.Services.AddScoped<IMainService<Type_Utilisation, int>>(sp => new WebService<Type_Utilisation, int>(sp.GetRequiredService<HttpClient>(), "type_utilisation"));
builder.Services.AddScoped<IMainService<Marque, int>>(sp => new WebService<Marque, int>(sp.GetRequiredService<HttpClient>(), "Marques"));
builder.Services.AddScoped<IMainService<Domaine_Excellence, int>>(sp => new WebService<Domaine_Excellence, int>(sp.GetRequiredService<HttpClient>(), "Domaine_Excellences"));
builder.Services.AddScoped<IMainService<Mot_Clef, int>>(sp => new WebService<Mot_Clef, int>(sp.GetRequiredService<HttpClient>(), "Mot_Clefs"));
builder.Services.AddScoped<IMainService<Contact_USMB, string>>(sp => new WebService<Contact_USMB, string>(sp.GetRequiredService<HttpClient>(), "Contact_USMB"));
builder.Services.AddScoped<IMainService<Fonction, int>>(sp => new WebService<Fonction, int>(sp.GetRequiredService<HttpClient>(), "Fonction"));
builder.Services.AddScoped<IMainService<Contact_USMB, int>>(sp => new WebService<Contact_USMB, int>(sp.GetRequiredService<HttpClient>(), "Contact_USMB"));
builder.Services.AddScoped<IMainService<Contact_USMBUpdateModel, int>>(sp => new WebService<Contact_USMBUpdateModel, int>(sp.GetRequiredService<HttpClient>(), "Contact_USMB"));
builder.Services.AddScoped<IMainService<Unite_Oeuvre, int>>(sp => new WebService<Unite_Oeuvre, int>(sp.GetRequiredService<HttpClient>(), "Unite_Oeuvres"));
builder.Services.AddScoped<IMainService<Fonctionnalite, int>>(sp => new WebService<Fonctionnalite, int>(sp.GetRequiredService<HttpClient>(), "Fonctionnalites"));

builder.Services.AddScoped<SearchService>();
await builder.Build().RunAsync();
