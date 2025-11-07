using Microsoft.EntityFrameworkCore;
using System;
using USMB_TECH.Models;
using USMB_TECH.Models.EntityFramework;
using USMB_TECH.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMainRepository<Laboratoire, string>, LaboratoireManager>();
builder.Services.AddScoped<IMainRepository<Plateforme, int>, PlateformeManager>();
builder.Services.AddScoped<IMainRepository<Thematique, int>, ThematiqueManager>();
builder.Services.AddScoped<IMainRepository<Prestation, int>, PrestationManager>();
builder.Services.AddScoped<IMainRepository<Contact_USMB, int>, Contact_USMBManager>();
builder.Services.AddScoped<IMainRepository<Equipement, int>, EquipementManager>();
builder.Services.AddDbContext<UsmbTechDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("UsmbTechDbContext")));

//builder.Services.AddAutoMapper(cfg =>
//{


//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
