using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace USMB_TECH.Models.EntityFramework;

public partial class UsmbTechDbContext : DbContext
{
    public DbSet<Adresse> Adresses { get; set; }    
    public DbSet<Associer> Associers { get; set; }
    public DbSet<Consommable> Consommables { get; set; }
    public DbSet<Consommer> Consommers { get; set; }
    public DbSet<Contact_USMB> Contact_USMBs { get; set; }
    public DbSet<Designer> Designers { get; set; }
    public DbSet<Equipement> Equipements { get; set; }
    public DbSet<Est_Lier> Est_Liers { get; set; }
    public DbSet<Exemple_Utilisation> Exemple_Utilisations { get; set; }
    public DbSet<Exposer> Exposers { get; set; }
    public DbSet<Fonction> Fonctions { get; set; }
    public DbSet<Fonctionalite> Functionalites { get; set; }
    public DbSet<Fournir> Fournirs { get; set; }
    public DbSet<Gerer> Gerers { get; set; }
    public DbSet<Laboratoire> Laboratoires { get; set; }
    public DbSet<Marque> Marques { get; set; }
    public DbSet<Modele> Modeles { get; set; }
    public DbSet<Mot_Clef> Mot_Clefs { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Plateforme> Plateformes { get; set; }
    public DbSet<Posseder> Posseders { get; set; }
    public DbSet<Presenter> Presenters { get; set; }    
    public DbSet<Prestation> Prestations { get; set; }
    public DbSet<Prise_Contact> Prise_Contacts { get; set; }
    public DbSet<Referencer> Referencers { get; set; }
    public DbSet<Specifier> Specifiers { get; set; }
    public DbSet<Thematique> Thematiques { get; set; }
    public DbSet<Type_Client> Type_Clients { get; set; }
    public DbSet<Type_Equipement> Type_Equipements { get; set; }
    public DbSet<Type_Prestation>Type_Prestations { get; set; }
    public DbSet<Unite> Unites { get; set; }
    public DbSet<Unite_Oeuvre> Unite_Oeuvres { get; set; }
    public UsmbTechDbContext()
    {
    }

    public UsmbTechDbContext(DbContextOptions<UsmbTechDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=usmbTechDB; uid=s213;password=dN8QKrYi;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
