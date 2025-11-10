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

builder.Services.AddScoped<IMainService<Equipement, int>>(eq => new WebService<Equipement, int>());
builder.Services.AddScoped<IMainService<Laboratoire, string>>(eq => new WebService<Laboratoire, string>());
builder.Services.AddScoped<IMainService<Plateforme, int>>(eq => new WebService<Plateforme, int>());
builder.Services.AddScoped<IMainService<Prestation, int>>(eq => new WebService<Prestation, int>());
builder.Services.AddScoped<IMainService<Thematique, int>>(eq => new WebService<Thematique, int>());

await builder.Build().RunAsync();
