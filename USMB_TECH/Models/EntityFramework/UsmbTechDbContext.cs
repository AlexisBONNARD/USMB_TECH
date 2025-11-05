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
        => optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=usmbTechDB; uid=s213;password=dN8QKrYi;SearchPath=usmbTech;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresse>(e =>
        {
            e.HasKey(e => e.Id_Adresse).HasName("adresse_pkey");

            e.HasMany(d => d.Laboratoires_campus)
                .WithOne(p => p.Adresse_campusNavigation)
                .HasForeignKey(d => d.Id_Adresse_Campus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratoire_id_adresse_campus_fkey");
            e.HasMany(d => d.Laboratoires_labo)
                .WithOne(p => p.Adresse_laboNavigation)
                .HasForeignKey(d => d.Id_Adresse_Labo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratoire_id_adresse_labo_fkey");
        });

        modelBuilder.Entity<Associer>(e =>
        {
            e.HasKey(e => new { e.Id_Contact, e.Id_Plateforme }).HasName("associer_pkey");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Associers)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("associer_id_contact_fkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Associers)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("associer_id_plateforme_fkey");
        });

        modelBuilder.Entity<Consommable>(e =>
        {
            e.HasKey(e => e.Id_Consommable).HasName("consommable_pkey");

            e.HasMany(d => d.Consommers)
                .WithOne(p => p.ConsommableNavigation)
                .HasForeignKey(d => d.Id_Consommable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommer_id_consommable_fkey");

            e.HasOne(d => d.UniteNavigation)
                .WithMany(p => p.Unites)
                .HasForeignKey(d => d.Id_Unite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommable_id_unite_fkey");
        });

        modelBuilder.Entity<Consommer>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Consommable }).HasName("consommer_pkey");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Consommers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommer_id_equipement_fkey");

            e.HasOne(d => d.ConsommableNavigation)
                .WithMany(p => p.Consommers)
                .HasForeignKey(d => d.Id_Consommable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommer_id_consommable_fkey");
        });

        modelBuilder.Entity<Contact_USMB>(e =>
        {
            e.HasKey(e => e.Id_Contact).HasName("contact_usmb_pkey");
            e.HasMany(d => d.Associers)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("associer_id_contact_fkey");

            e.HasMany(d =>d.Prestations)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_contact_fkey");

            e.HasMany(d => d.Referencers)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referencer_id_contact_fkey");

            e.HasOne(d =>d.LaboratoireNavigation)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_usmb_nom_court_fkey");

            e.HasOne(d => d.FonctionNavigation)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.Id_Fonction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_usmb_id_fonction_fkey");
        });

        modelBuilder.Entity<Designer>(e =>
        {
            e.HasKey(e => new { e.Id_Mot_Clef, e.Nom_Court }).HasName("designer_pkey");
            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Designers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("designer_nom_court_fkey");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Designers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("designer_id_mot_clef_fkey");

        });

        modelBuilder.Entity<Equipement>(e => {
            e.HasKey(e => new { e.Id_Equipement }).HasName("equipement_pkey");

            e.HasMany(d => d.Fournirs)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fournir_id_equipement_fkey");

            e.HasMany(d => d.Consommers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommer_id_equipement_fkey");

            e.HasMany(d => d.Posseders)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posseder_id_equipement_fkey");  

            e.HasMany(d => d.Exemple_Utilisations)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exemple_utilisation_id_equipement_fkey");

            e.HasMany(d =>d.Referencers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referencer_id_equipement_fkey");

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_equipement_fkey");

            e.HasMany(d => d.Photos)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_id_equipement_fkey");

            e.HasOne(d => d.Type_EquipementNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Type_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_type_equipement_fkey");

            e.HasOne(d => d.MarqueNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_marque_fkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_plateforme_fkey");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
