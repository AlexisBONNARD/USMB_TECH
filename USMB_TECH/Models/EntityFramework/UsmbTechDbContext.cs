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
    public DbSet<Domaine_Excellence> Domaine_Excellences { get; set; }
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
    public DbSet<Pole_Expertise> Pole_Expertises { get; set; }
    public DbSet<Posseder> Posseders { get; set; }
    public DbSet<Preciser> Precisers { get; set; }
    public DbSet<Presenter> Presenters { get; set; }
    public DbSet<Prestation> Prestations { get; set; }
    public DbSet<Prise_Contact> Prise_Contacts { get; set; }
    public DbSet<Qualifier> Qualifiers { get; set; }
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
        => optionsBuilder.UseNpgsql("Server=usmb-tech.postgres.database.azure.com;Database=usmbTechDb;Port=5432;User Id=UsmbTech;Password=dN8QKrYi;Ssl Mode=Require;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("usmbTech");

        modelBuilder.Entity<Adresse>(e =>
        {
            e.HasKey(e => e.Id_Adresse).HasName("pk_adresse");

            e.Property(e => e.Id_Adresse).ValueGeneratedOnAdd();

            e.HasMany(d => d.Laboratoires_campus)
                .WithOne(p => p.Adresse_campusNavigation)
                .HasForeignKey(d => d.Id_Adresse_Campus)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_adresse_laboratoire_campus");

            e.HasMany(d => d.Laboratoires_labo)
                .WithOne(p => p.Adresse_laboNavigation)
                .HasForeignKey(d => d.Id_Adresse_Labo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_adresse_laboratoire_labo");
        });

        modelBuilder.Entity<Associer>(e =>
        {
            e.HasKey(e => new { e.Id_Contact, e.Id_Pole_Expertise }).HasName("pk_associer");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Associers)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_associer_contactUSMB");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Associers)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_associer_pole_expertise");
        });

        modelBuilder.Entity<Consommable>(e =>
        {
            e.HasKey(e => e.Id_Consommable).HasName("pk_consommable");

            e.Property(e => e.Id_Consommable).ValueGeneratedOnAdd();

            e.HasMany(d => d.Consommers)
                .WithOne(p => p.ConsommableNavigation)
                .HasForeignKey(d => d.Id_Consommable)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_consommable_consommer");

            e.HasOne(d => d.UniteNavigation)
                .WithMany(p => p.Consommables)
                .HasForeignKey(d => d.Id_Unite)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_consommable_unite");
        });

        modelBuilder.Entity<Consommer>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Consommable }).HasName("pk_consommer");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Consommers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_consommer_equipement");

            e.HasOne(d => d.ConsommableNavigation)
                .WithMany(p => p.Consommers)
                .HasForeignKey(d => d.Id_Consommable)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_consommer_consommable");
        });

        modelBuilder.Entity<Contact_USMB>(e =>
        {
            e.HasKey(e => e.Id_Contact).HasName("pk_contact_USMB");

            e.Property(e => e.Id_Contact).ValueGeneratedOnAdd();

            e.HasMany(d => d.Associers)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contact_USMB_associer");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contact_USMB_prestation");

            e.HasMany(d => d.Referencers)
                .WithOne(p => p.Contact_USMBNavigation)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contact_USMB_referencer");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contact_USMB_laboratoire");

            e.HasOne(d => d.FonctionNavigation)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.Id_Fonction)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contact_USBM_fonction");
        });

        modelBuilder.Entity<Designer>(e =>
        {
            e.HasKey(e => new { e.Id_Mot_Clef, e.Nom_Court }).HasName("pk_designer");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Designers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_designer_laboratoire");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Designers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_designer_mot_clef");

        });

        modelBuilder.Entity<Domaine_Excellence>(e =>
        {
            e.HasKey(e => e.Id_Domaine_Excellence).HasName("pk_domaine_excellence");

            e.Property(e => e.Id_Domaine_Excellence).ValueGeneratedOnAdd();

            e.HasMany(d => d.Photos)
                .WithOne(p => p.Domaine_ExcellenceNavigation)
                .HasForeignKey(d => d.Id_Domaine_Excellence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_domaine_excellence_photo");

            e.HasMany(d => d.Pole_Expertises)
                .WithOne(p => p.Domaine_ExcellenceNavigation)
                .HasForeignKey(d => d.Id_Domaine_Excellence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_domaine_excellence_pole_expertise");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Domaine_ExcellenceNavigation)
                .HasForeignKey(d => d.Id_Domaine_Excellence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_domaine_excellence_prestation");

        });

        modelBuilder.Entity<Equipement>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement }).HasName("pk_equipement");

            e.Property(e => e.Id_Equipement).ValueGeneratedOnAdd();

            e.HasMany(d => d.Fournirs)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_fournir");

            e.HasMany(d => d.Consommers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_consommer");

            e.HasMany(d => d.Posseders)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_posseder");

            e.HasMany(d => d.Exemple_Utilisations)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_exemple_utilisation");

            e.HasMany(d => d.Referencers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_referencer");

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_prise_contact");

            e.HasMany(d => d.Photos)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_photo");

            e.HasOne(d => d.Type_EquipementNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Type_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_type_equipement");

            e.HasOne(d => d.ModeleNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Modele)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_modele");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Equipements)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_pole_expertise");

            e.HasMany(d => d.Qualifiers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_qualifier");

            e.HasMany(d => d.Exposers)
                .WithOne(p => p.EquipementNavigation)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_equipement_exposer");
        });

        modelBuilder.Entity<Est_Lier>(e =>
        {
            e.HasKey(e => new { e.Nom_Court, e.Id_Thematique }).HasName("pk_est_lier");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Est_Liers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_est_lier_laboratoire");

            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Est_Liers)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_est_lier_thematique");
        });

        modelBuilder.Entity<Exemple_Utilisation>(e =>
        {
            e.HasKey(e => e.Id_Exemple_Utilisation).HasName("pk_exemple_utilisation");

            e.Property(e => e.Id_Exemple_Utilisation).ValueGeneratedOnAdd();

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Exemple_Utilisations)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_exemple_utilisation_equipement");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Exemple_Utilisations)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_exemple_utilisation_pole_expertise");
        });

        modelBuilder.Entity<Exposer>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Thematique }).HasName("pk_exposer");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Exposers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_exposer_equipement");

            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Exposers)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_exposer_thematique");
        });

        modelBuilder.Entity<Fonction>(e =>
        {
            e.HasKey(e => e.Id_Fonction).HasName("pk_fonction");

            e.Property(e => e.Id_Fonction).ValueGeneratedOnAdd();

            e.HasMany(d => d.Contacts)
                .WithOne(p => p.FonctionNavigation)
                .HasForeignKey(d => d.Id_Fonction)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_fonction_contact");
        });

        modelBuilder.Entity<Fonctionalite>(e =>
        {
            e.HasKey(e => e.Id_Fonctionalite).HasName("pk_fonctionalite");

            e.Property(e => e.Id_Fonctionalite).ValueGeneratedOnAdd();

            e.HasMany(d => d.Posseders)
                .WithOne(p => p.FonctionaliteNavigation)
                .HasForeignKey(d => d.Id_Fonctionalite)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_fonctionalite_posseder");
        });

        modelBuilder.Entity<Fournir>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Prestation }).HasName("pk_fournir");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Fournirs)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_fournir_equipement");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Fournirs)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_fournir_prestation");
        });

        modelBuilder.Entity<Gerer>(e =>
        {
            e.HasKey(e => new { e.Id_Pole_Expertise, e.Nom_Court }).HasName("pk_gerer");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Gerers)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_gerer_pole_expertise");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Gerers)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_gerer_laboratoire");
        });

        modelBuilder.Entity<Laboratoire>(e =>
        {
            e.HasKey(e => new { e.Nom_Court }).HasName("pk_laboratoire");

            e.HasMany(d => d.Contacts)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_contact_USMB");

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_prestation");

            e.HasMany(d => d.Est_Liers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_est_lier");

            e.HasMany(d => d.Designers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_designer");

            e.HasMany(d => d.Gerers)
                .WithOne(p => p.LaboratoireNavigation)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_gerer");

            e.HasOne(d => d.Adresse_campusNavigation)
                .WithMany(p => p.Laboratoires_campus)
                .HasForeignKey(d => d.Id_Adresse_Campus)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_adresse_campus");

            e.HasOne(d => d.Adresse_laboNavigation)
                .WithMany(p => p.Laboratoires_labo)
                .HasForeignKey(d => d.Id_Adresse_Labo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_laboratoire_adresse_labo");
        });

        modelBuilder.Entity<Marque>(e =>
        {
            e.HasKey(e => e.Id_Marque).HasName("pk_marque");

            e.Property(e => e.Id_Marque).ValueGeneratedOnAdd();

            e.HasMany(d => d.Modeles)
                .WithOne(p => p.MarqueNavigation)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_marque_modele");
        });

        modelBuilder.Entity<Modele>(e =>
        {
            e.HasKey(e => e.Id_Modele).HasName("pk_modele");

            e.Property(e => e.Id_Modele).ValueGeneratedOnAdd();

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.ModeleNavigation)
                .HasForeignKey(d => d.Id_Modele)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_modele_equipement");

            e.HasOne(d => d.MarqueNavigation)
                .WithMany(p => p.Modeles)
                .HasForeignKey(d => d.Id_Marque)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_modele_marque");
        });

        modelBuilder.Entity<Mot_Clef>(e =>
        {
            e.HasKey(e => e.Id_Mot_Clef).HasName("pk_mot_clef");

            e.Property(e => e.Id_Mot_Clef).ValueGeneratedOnAdd();

            e.HasMany(d => d.Designers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mot_clef_designer");

            e.HasMany(d => d.Specifiers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mot_clef_specifier");

            e.HasMany(d => d.Precisers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mot_clef_preciser");

            e.HasMany(d => d.Qualifiers)
                .WithOne(p => p.Mot_ClefNavigation)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mot_clef_qualifier");
        });

        modelBuilder.Entity<Photo>(e =>
        {
            e.HasKey(e => e.Id_Photo).HasName("pk_photo");

            e.Property(e => e.Id_Photo).ValueGeneratedOnAdd();

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_photo_equipement");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_photo_pole_expertise");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_photo_prestation");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_photo_pole_expertise");

            e.HasOne(d => d.Domaine_ExcellenceNavigation)
                .WithMany(p => p.Photos)
                .HasForeignKey(d => d.Id_Domaine_Excellence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_photo_pole_expertise");

            // ✅ Contrainte d’exclusion : Une photo ne peut être liée qu’à un pôle d'expertise, un laboratoire, une prestation, à un domaine d'excellence ou à un équipement.
            e.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_Photo_EquipementOuPole_ExpertiseOuDomaine_Excellence",
                    @"(
                        ((id_equipement IS NOT NULL)::int +
                        (id_pole_expertise IS NOT NULL)::int +
                        (id_prestation IS NOT NULL)::int +
                        (nom_court IS NOT NULL)::int +
                        (id_domaine_excellence IS NOT NULL)::int)
                    ) = 1"
                );
            });
        });

        modelBuilder.Entity<Pole_Expertise>(e =>
        {
            e.HasKey(e => e.Id_Pole_Expertise).HasName("pk_pole_expertise");

            e.Property(e => e.Id_Pole_Expertise).ValueGeneratedOnAdd();

            e.HasMany(d => d.Associers)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_associer");

            e.HasMany(d => d.Gerers)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_gerer");

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_equipement");

            e.HasMany(d => d.Exemple_Utilisations)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_exemple_utilisation");

            e.HasMany(d => d.Photos)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_photo");

            e.HasMany(d => d.Presenters)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_presenter");

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_prise_contact");

            e.HasMany(d => d.Specifiers)
                .WithOne(p => p.Pole_ExpertiseNavigation)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pole_expertise_specifier");
        });

        modelBuilder.Entity<Posseder>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Fonctionalite }).HasName("pk_posseder");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Posseders)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_posseder_equipement");

            e.HasOne(d => d.FonctionaliteNavigation)
                .WithMany(p => p.Posseders)
                .HasForeignKey(d => d.Id_Fonctionalite)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_posseder_fonctionalite");
        });

        modelBuilder.Entity<Preciser>(e =>
        {
            e.HasKey(e => new { e.Id_Prestation, e.Id_Mot_Clef }).HasName("pk_preciser");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Precisers)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_preciser_prestation");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Precisers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_preciser_mot_clef");
        });

        modelBuilder.Entity<Presenter>(e =>
        {
            e.HasKey(e => new { e.Id_Pole_Expertise, e.Id_Prestation }).HasName("pk_presenter");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Presenters)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_presenter_pole_expertise");

            e.HasOne(d => d.PrestationNavigation)
                .WithMany(p => p.Presenters)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_presenter_prestation");
        });

        modelBuilder.Entity<Prestation>(e =>
        {
            e.HasKey(e => new { e.Id_Prestation }).HasName("pk_prestation");

            e.Property(e => e.Id_Prestation).ValueGeneratedOnAdd();

            e.HasMany(d => d.Fournirs)
                .WithOne(p => p.PrestationNavigation)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_fournir_prestation");

            e.HasMany(d => d.Presenters)
                .WithOne(p => p.PrestationNavigation)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_presenter_prestation");

            e.HasMany(d => d.Photos)
                .WithOne(p => p.PrestationNavigation)
                .HasForeignKey(d => d.Id_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_photo");

            e.HasOne(d => d.Unite_OeuvreNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Unite_Oeuvre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_unite_oeuvre");

            e.HasOne(d => d.Type_PrestationNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Type_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_type_prestation");

            e.HasOne(d => d.LaboratoireNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Nom_Court)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_laboratoire");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_contact_usmb");

            e.HasOne(d => d.Domaine_ExcellenceNavigation)
                .WithMany(p => p.Prestations)
                .HasForeignKey(d => d.Id_Domaine_Excellence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prestation_domaine_excellence");
        });

        modelBuilder.Entity<Prise_Contact>(e =>
        {
            e.HasKey(e => e.Num_Prise_Contact).HasName("pk_prise_contact");

            e.Property(e => e.Num_Prise_Contact).ValueGeneratedOnAdd();

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prise_contact_equipement");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prise_contact_pole_expertise");

            e.HasOne(d => d.Type_ClientNavigation)
                .WithMany(p => p.Prise_Contacts)
                .HasForeignKey(d => d.Id_Type_Client)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_prise_contact_type_client");

            // ✅ Contrainte d’exclusion : Une prise de contact ne peut être liée qu’à un pôle d'expertise ou à un équipement, pas les deux.
            e.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_PriseContact_EquipementOuPole_Expertise",
                    "(\"id_equipement\" IS NULL) <> (\"id_pole_expertise\" IS NULL)"
                );
            });
        });

        modelBuilder.Entity<Qualifier>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Mot_Clef }).HasName("pk_qualifier");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Qualifiers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_qualifier_equipement");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Qualifiers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_qualifier_mot_clef");
        });

        modelBuilder.Entity<Referencer>(e =>
        {
            e.HasKey(e => new { e.Id_Equipement, e.Id_Contact }).HasName("pk_referencer");

            e.HasOne(d => d.EquipementNavigation)
                .WithMany(p => p.Referencers)
                .HasForeignKey(d => d.Id_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_referencer_equipement");

            e.HasOne(d => d.Contact_USMBNavigation)
                .WithMany(p => p.Referencers)
                .HasForeignKey(d => d.Id_Contact)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_referencer_contact");
        });

        modelBuilder.Entity<Specifier>(e =>
        {
            e.HasKey(e => new { e.Id_Mot_Clef, e.Id_Pole_Expertise }).HasName("pk_specifier");

            e.HasOne(d => d.Mot_ClefNavigation)
                .WithMany(p => p.Specifiers)
                .HasForeignKey(d => d.Id_Mot_Clef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_specifier_mot_clef");

            e.HasOne(d => d.Pole_ExpertiseNavigation)
                .WithMany(p => p.Specifiers)
                .HasForeignKey(d => d.Id_Pole_Expertise)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_specifier_pole_expertise");
        });



        modelBuilder.Entity<Thematique>(e =>
        {
            e.HasKey(e => e.Id_Thematique).HasName("pk_thematique");

            e.Property(e => e.Id_Thematique).ValueGeneratedOnAdd();

            e.HasMany(d => d.Est_Liers)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_thematique_est_lier");

            e.HasMany(d => d.Exposers)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_thematique_exposer");

            e.HasMany(d => d.Thematiques)
                .WithOne(p => p.ThematiqueNavigation)
                .HasForeignKey(d => d.Id_Thematique)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_thematique_sous_thematique");

            e.HasOne(d => d.ThematiqueNavigation)
                .WithMany(p => p.Thematiques)
                .HasForeignKey(d => d.Id_Thematique)
                .HasConstraintName("fk_thematique_thematique_parente");

        });

        modelBuilder.Entity<Type_Client>(e =>
        {
            e.HasKey(e => e.Id_Type_Client).HasName("pk_type_client");

            e.Property(e => e.Id_Type_Client).ValueGeneratedOnAdd();

            e.HasMany(d => d.Prise_Contacts)
                .WithOne(p => p.Type_ClientNavigation)
                .HasForeignKey(d => d.Id_Type_Client)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_type_client_prise_contact");
        });

        modelBuilder.Entity<Type_Equipement>(e =>
        {
            e.HasKey(e => e.Id_Type_Equipement).HasName("pk_type_equipement");

            e.Property(e => e.Id_Type_Equipement).ValueGeneratedOnAdd();

            e.HasMany(d => d.Equipements)
                .WithOne(p => p.Type_EquipementNavigation)
                .HasForeignKey(d => d.Id_Type_Equipement)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_type_equipement_equipement");
        });

        modelBuilder.Entity<Type_Prestation>(e =>
        {
            e.HasKey(e => e.Id_Type_Prestation).HasName("pk_type_prestation");

            e.Property(e => e.Id_Type_Prestation).ValueGeneratedOnAdd();

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Type_PrestationNavigation)
                .HasForeignKey(d => d.Id_Type_Prestation)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_type_prestation_prestation");
        });

        modelBuilder.Entity<Unite>(e =>
        {
            e.HasKey(e => e.Id_Unite).HasName("pk_unite");

            e.Property(e => e.Id_Unite).ValueGeneratedOnAdd();

            e.HasMany(d => d.Consommables)
                .WithOne(p => p.UniteNavigation)
                .HasForeignKey(d => d.Id_Unite)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_unite_consommable");
        });

        modelBuilder.Entity<Unite_Oeuvre>(e =>
        {
            e.HasKey(e => e.Id_Unite_Oeuvre).HasName("pk_unite_oeuvre");

            e.Property(e => e.Id_Unite_Oeuvre).ValueGeneratedOnAdd();

            e.HasMany(d => d.Prestations)
                .WithOne(p => p.Unite_OeuvreNavigation)
                .HasForeignKey(d => d.Id_Unite_Oeuvre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_unite_oeuvre_prestation");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
