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
    public DbSet<Type_Prestation> Type_Prestations { get; set; }
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

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_contact_fkey");

            e.HasMany(d => d.Referencers)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referencer_id_contact_fkey");

            e.HasOne(d => d.LaboratoireNavigation)
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

        modelBuilder.Entity<Equipement>(e =>
        {
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

            e.HasMany(d => d.Referencers)
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

        modelBuilder.Entity<Est_Lier>(e =>
        {
            e.HasKey(e => new { e.Nom_Court, e.Id_Thematique }).HasName("est_lier_pkey");
            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Est_Liers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("est_lier_nom_court_fkey");

            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Est_Liers)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("est_lier_id_thematique_fkey");
        });

        modelBuilder.Entity<Exemple_Utilisation>(e =>
        {
            e.HasKey(e => e.Id_Exemple_Utilisation).HasName("exemple_utilisation_pkey");
            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Exemple_Utilisations)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exemple_utilisation_id_equipement_fkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Exemple_Utilisations)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exemple_utilisation_id_plateforme_fkey");
        });

        modelBuilder.Entity<Exposer>(e =>
        {
            e.HasKey(e => new { e.Id_Plateforme, e.Id_Thematique }).HasName("exposer_pkey");
            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Exposers)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exposer_id_plateforme_fkey");
            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Exposers)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exposer_id_thematique_fkey");
        });

        modelBuilder.Entity<Fonction>(e =>
        {
            e.HasKey(e => e.Id_Fonction).HasName("fonction_pkey");

            e.HasMany(d => d.Contacts)
                .WithOne(p => p.FonctionNavigation)
                .HasForeignKey(d => d.Id_Fonction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_usmb_id_fonction_fkey");
        });

        modelBuilder.Entity<Fonctionalite>(e =>
        {
            e.HasKey(e => e.Id_Fonctionalite).HasName("fonctionalite_pkey");

            e.HasMany(d => d.Posseders)
                .WithOne(p => p.FonctionaliteNavigation)
                .HasForeignKey(d => d.Id_Fonctionalite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posseder_id_fonctionalite_fkey");

        });

        modelBuilder.Entity<Fournir>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Prestation }).HasName("fournir_pkey");
            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Fournirs)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fournir_id_equipement_fkey");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Fournirs)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fournir_id_prestation_fkey");
        });

        modelBuilder.Entity<Gerer>(e =>
        {
            e.HasKey(e => new { e.Id_Plateforme, e.Nom_Court }).HasName("gerer_pkey");
            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Gerers)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gerer_id_plateforme_fkey");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Gerers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gerer_nom_court_fkey");
        });

        modelBuilder.Entity<Laboratoire>(e =>
        {
            e.HasKey(e => new { e.Nom_Court }).HasName("laboratoire_pkey");

            e.HasMany(d => d.Contacts)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_usmb_nom_court_fkey");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_nom_court_fkey");

            e.HasMany(d => d.Est_Liers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("est_lier_nom_court_fkey");

            e.HasMany(d => d.Designers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("designer_nom_court_fkey");

            e.HasMany(d => d.Gerers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gerer_nom_court_fkey");

            e.HasOne(d => d.Adresse_campusNavigation)
                .WithMany(p => p.Laboratoires_campus)
                .HasForeignKey(d => d.Id_Adresse_Campus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratoire_id_adresse_campus_fkey");

            e.HasOne(d => d.Adresse_laboNavigation)
                .WithMany(p => p.Laboratoires_labo)
                .HasForeignKey(d => d.Id_Adresse_Labo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("laboratoire_id_adresse_labo_fkey");
        });

        modelBuilder.Entity<Marque>(e =>
        {
            e.HasKey(e => e.Id_Marque).HasName("marque_pkey");

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.MarqueNavigation)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_marque_fkey");

            e.HasMany(d => d.Modeles)
                .WithOne(p => p.MarqueNavigation)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modele_id_marque_fkey");
        });

        modelBuilder.Entity<Modele>(e =>
        {
            e.HasKey(e => e.Id_Modele).HasName("modele_pkey");

            e.HasOne(d => d.MarqueNavigation)
                .WithMany(p => p.Modeles)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modele_id_marque_fkey");
        });

        modelBuilder.Entity<Mot_Clef>(e =>
        {
            e.HasKey(e => e.Id_Mot_Clef).HasName("mot_clef_pkey");

            e.HasMany(d => d.Designers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("designer_id_mot_clef_fkey");

            e.HasMany(d => d.Specifiers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specifier_id_mot_clef_fkey");
        });

        modelBuilder.Entity<Photo>(e =>
        {
            e.HasKey(e => e.Id_Photo).HasName("photo_pkey");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_id_equipement_fkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_id_plateforme_fkey");
        });

        modelBuilder.Entity<Plateforme>(e =>
        {
            e.HasKey(e => e.Id_Plateforme).HasName("plateforme_pkey");

            e.HasMany(d => d.Associers)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("associer_id_plateforme_fkey");

            e.HasMany(d => d.Exposers)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exposer_id_plateforme_fkey");

            e.HasMany(d => d.Gerers)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("gerer_id_plateforme_fkey");

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_plateforme_fkey");

            e.HasMany(d => d.Exemple_Utilisations)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exemple_utilisation_id_plateforme_fkey");

            e.HasMany(d => d.Photos)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_id_plateforme_fkey");

            e.HasMany(d => d.Presenters)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("presenter_id_plateforme_fkey");

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_plateforme_fkey");

            e.HasMany(d => d.Specifiers)
                .WithOne(p => p.PlateformeNavigation)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specifier_id_plateforme_fkey");

        });

        modelBuilder.Entity<Posseder>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Fonctionalite }).HasName("posseder_pkey");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Posseders)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posseder_id_equipement_fkey");

            e.HasOne(d => d.FonctionaliteNavigation)
                .WithMany(p => p.Posseders)
                .HasForeignKey(d => d.Id_Fonctionalite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posseder_id_fonctionalite_fkey");
        });

        modelBuilder.Entity<Presenter>(e =>
        {
            e.HasKey(e => new { e.Id_Plateforme, e.Id_Prestation }).HasName("presenter_pkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Presenters)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("presenter_id_plateforme_fkey");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Presenters)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("presenter_id_prestation_fkey");
        });

        modelBuilder.Entity<Prestation>(e =>
        {
            e.HasKey(e => new { e.Id_Prestation }).HasName("prestation_pkey");
            e.HasMany(d => d.Fournirs)
                .WithOne(p => p.PrestationNavigation)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fournir_id_prestation_fkey");

            e.HasMany(d => d.Presenters)
            .WithOne(p => p.PrestationNavigation)
            .HasForeignKey(d => d.Id_Prestation)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("presenter_id_prestation_fkey");

            e.HasOne(d => d.Unite_OeuvreNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Unite_Oeuvre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_unite_oeuvre_fkey");

            e.HasOne(d => d.Type_PrestationNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Type_Prestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_type_prestation_fkey");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_nom_court_fkey");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_contact_fkey");

        });

        modelBuilder.Entity<Prise_Contact>(e =>
        {
            e.HasKey(e => e.Num_Prise_Contact).HasName("prise_contact_pkey");
            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_equipement_fkey");
            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_plateforme_fkey");
            e.HasOne(d => d.Type_ClientNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Type_Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_type_client_fkey");
        });

        modelBuilder.Entity<Referencer>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Contact }).HasName("referencer_pkey");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Referencers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referencer_id_equipement_fkey");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Referencers)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("referencer_id_contact_fkey");
        });

        modelBuilder.Entity<Specifier>(e =>
        {
            e.HasKey(e => new { e.Id_Mot_Clef, e.Id_Plateforme }).HasName("specifier_pkey");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Specifiers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specifier_id_mot_clef_fkey");

            e.HasOne(d => d.PlateformeNavigation)
                .WithMany(p => p.Specifiers)
                .HasForeignKey(d => d.Id_Plateforme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("specifier_id_plateforme_fkey");
        });

        modelBuilder.Entity<Thematique>(e =>
        {
            e.HasKey(e => e.Id_Thematique).HasName("thematique_pkey");

            e.HasMany(d => d.Est_Liers)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("est_lier_id_thematique_fkey");

            e.HasMany(d => d.Exposers)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exposer_id_thematique_fkey");

            e.HasMany(d => d.Thematiques)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exposer_id_thematique_fkey");

            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Thematiques)
                .HasForeignKey(d => d.Id_Thematique)
                .HasConstraintName("thematique_id_thematique_parente_fkey");

        });

        modelBuilder.Entity<Type_Client>(e =>
        {
            e.HasKey(e => e.Id_Type_Client).HasName("type_client_pkey");

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.Type_ClientNavigation)
                .HasForeignKey(d => d.Id_Type_Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prise_contact_id_type_client_fkey");
        });

        modelBuilder.Entity<Type_Equipement>(e =>
        {
            e.HasKey(e => e.Id_Type_Equipement).HasName("type_equipement_pkey");

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.Type_EquipementNavigation)
                .HasForeignKey(d => d.Id_Type_Equipement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipement_id_type_equipement_fkey");
        });

        modelBuilder.Entity<Type_Prestation>(e =>
        {
            e.HasKey(e => e.Id_Type_Prestation).HasName("type_prestation_pkey");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Type_PrestationNavigation)
                .HasForeignKey(d => d.Id_Type_Prestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_type_prestation_fkey");
        });

        modelBuilder.Entity<Unite>(e =>
        {
            e.HasKey(e => e.Id_Unite).HasName("unite_pkey");

            e.HasMany(d => d.Consommables)
                .WithOne(p => p.UniteNavigation)
                .HasForeignKey(d => d.Id_Unite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consommable_id_unite_fkey");
        });

        modelBuilder.Entity<Unite_Oeuvre>(e =>
        {
            e.HasKey(e => e.Id_Unite_Oeuvre).HasName("unite_oeuvre_pkey");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Unite_OeuvreNavigation)
                .HasForeignKey(d => d.Id_Unite_Oeuvre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_unite_oeuvre_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
