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

builder.Services.AddScoped<AdminState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMainService<Equipement, int>>(eq => new WebService<Equipement, int>("Equipements"));
builder.Services.AddScoped<IMainService<Laboratoire, string>>(eq => new WebService<Laboratoire, string>("Laboratoires"));
builder.Services.AddScoped<IMainService<LaboratoireUpdate, string>>(eq => new WebService<LaboratoireUpdate, string>("Laboratoires"));
builder.Services.AddScoped<IMainService<Pole_Expertise, int>>(eq => new WebService<Pole_Expertise, int>("Pole_Expertises"));
builder.Services.AddScoped<IMainService<Prise_Contact, int>>(eq => new WebService<Prise_Contact, int>("Prise_Contacts"));
builder.Services.AddScoped<IMainService<Prestation, int>>(eq => new WebService<Prestation, int>("Prestations"));
builder.Services.AddScoped<IMainService<PrestationUpdateModel, int>>(eq => new WebService<PrestationUpdateModel, int>("Prestations"));
builder.Services.AddScoped<IMainService<Thematique, int>>(eq => new WebService<Thematique, int>("Thematique"));
builder.Services.AddScoped<IMainService<Type_Equipement, int>>(eq => new WebService<Type_Equipement, int>("Type_Equipements"));
builder.Services.AddScoped<IMainService<Type_Prestation, int>>(eq => new WebService<Type_Prestation, int>("Type_Prestations"));
builder.Services.AddScoped<IMainService<Marque, int>>(eq => new WebService<Marque, int>("Marques"));
builder.Services.AddScoped<IMainService<Domaine_Excellence, int>>(eq => new WebService<Domaine_Excellence, int>("Domaine_Excellences"));
builder.Services.AddScoped<IMainService<Mot_Clef, int>>(eq => new WebService<Mot_Clef, int>("Mot_Clefs"));
builder.Services.AddScoped<IMainService<Contact_USMB, string>>(eq => new WebService<Contact_USMB, string>("Contact_USMB"));
builder.Services.AddScoped<IMainService<Fonction, int>>(eq => new WebService<Fonction, int>("Fonction"));
builder.Services.AddScoped<IMainService<Contact_USMB, int>>(eq => new WebService<Contact_USMB, int>("Contact_USMB"));
builder.Services.AddScoped<IMainService<Contact_USMBUpdateModel, int>>(eq => new WebService<Contact_USMBUpdateModel, int>("Contact_USMB"));
builder.Services.AddScoped<IMainService<Unite_Oeuvre,int>>(eq => new WebService<Unite_Oeuvre, int>("Unite_Oeuvres"));
//builder.Services.AddHttpClient(); // Assurez-vous que HttpClient est disponible
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net") // URL de ton API
});
builder.Services.AddScoped<SearchService>();
await builder.Build().RunAsync();
