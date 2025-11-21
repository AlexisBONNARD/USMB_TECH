using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddScoped<IMainRepository<Type_Equipement, int>, Type_EquipementManager>();

// Enregistrement des managers
builder.Services.AddScoped<EquipementManager>();
builder.Services.AddScoped<LaboratoireManager>();
builder.Services.AddScoped<PlateformeManager>();
builder.Services.AddScoped<PrestationManager>();
builder.Services.AddScoped<ThematiqueManager>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<UsmbTechDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("UsmbTechDbContext")));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7264")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                          policy.WithOrigins("https://localhost:7093")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
