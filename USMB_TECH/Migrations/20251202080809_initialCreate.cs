using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "usmbTech");

            migrationBuilder.CreateTable(
                name: "adresse",
                schema: "usmbTech",
                columns: table => new
                {
                    id_adresse = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rue_adresse = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    complement_rue_adresse = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    code_postal_adresse = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    ville_adresse = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    pays_adresse = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adresse", x => x.id_adresse);
                });

            migrationBuilder.CreateTable(
                name: "fonction",
                schema: "usmbTech",
                columns: table => new
                {
                    id_fonction = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_fonction = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fonction", x => x.id_fonction);
                });

            migrationBuilder.CreateTable(
                name: "fonctionalite",
                schema: "usmbTech",
                columns: table => new
                {
                    id_fonctionalite = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_fonctionalite = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fonctionalite", x => x.id_fonctionalite);
                });

            migrationBuilder.CreateTable(
                name: "marque",
                schema: "usmbTech",
                columns: table => new
                {
                    id_marque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_marque = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marque", x => x.id_marque);
                });

            migrationBuilder.CreateTable(
                name: "mot_clef",
                schema: "usmbTech",
                columns: table => new
                {
                    id_mot_clef = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_mot_clef = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mot_clef", x => x.id_mot_clef);
                });

            migrationBuilder.CreateTable(
                name: "pole_expertise",
                schema: "usmbTech",
                columns: table => new
                {
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_pole_expertise = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description_pole_expertise = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    nom_contenu = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    url_contenu = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description_contenu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pole_expertise", x => x.id_pole_expertise);
                });

            migrationBuilder.CreateTable(
                name: "thematique",
                schema: "usmbTech",
                columns: table => new
                {
                    id_thematique = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_sous_thematique = table.Column<int>(type: "integer", nullable: false),
                    nom_thematique = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thematique", x => x.id_thematique);
                });

            migrationBuilder.CreateTable(
                name: "type_client",
                schema: "usmbTech",
                columns: table => new
                {
                    id_type_client = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_typeclient = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_client", x => x.id_type_client);
                });

            migrationBuilder.CreateTable(
                name: "type_equipement",
                schema: "usmbTech",
                columns: table => new
                {
                    id_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_equipement", x => x.id_type);
                });

            migrationBuilder.CreateTable(
                name: "type_prestation",
                schema: "usmbTech",
                columns: table => new
                {
                    id_type_prestation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_type_prestation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_prestation", x => x.id_type_prestation);
                });

            migrationBuilder.CreateTable(
                name: "unite",
                schema: "usmbTech",
                columns: table => new
                {
                    id_unite = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_unite = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unite", x => x.id_unite);
                });

            migrationBuilder.CreateTable(
                name: "unite_oeuvre",
                schema: "usmbTech",
                columns: table => new
                {
                    id_unite_oeuvre = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_unite_oeuvre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unite_oeuvre", x => x.id_unite_oeuvre);
                });

            migrationBuilder.CreateTable(
                name: "laboratoire",
                schema: "usmbTech",
                columns: table => new
                {
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    id_adresse_campus = table.Column<int>(type: "integer", nullable: false),
                    id_adresse_labo = table.Column<int>(type: "integer", nullable: false),
                    nom_long = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_laboratoire", x => x.nom_court);
                    table.ForeignKey(
                        name: "fk_laboratoire_adresse_campus",
                        column: x => x.id_adresse_campus,
                        principalSchema: "usmbTech",
                        principalTable: "adresse",
                        principalColumn: "id_adresse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_laboratoire_adresse_labo",
                        column: x => x.id_adresse_labo,
                        principalSchema: "usmbTech",
                        principalTable: "adresse",
                        principalColumn: "id_adresse",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "modele",
                schema: "usmbTech",
                columns: table => new
                {
                    id_modele = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_marque = table.Column<int>(type: "integer", nullable: false),
                    nom_modele = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modele", x => x.id_modele);
                    table.ForeignKey(
                        name: "fk_modele_marque",
                        column: x => x.id_marque,
                        principalSchema: "usmbTech",
                        principalTable: "marque",
                        principalColumn: "id_marque",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specifier",
                schema: "usmbTech",
                columns: table => new
                {
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    id_mot_clef = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specifier", x => new { x.id_mot_clef, x.id_pole_expertise });
                    table.ForeignKey(
                        name: "fk_specifier_mot_clef",
                        column: x => x.id_mot_clef,
                        principalSchema: "usmbTech",
                        principalTable: "mot_clef",
                        principalColumn: "id_mot_clef",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_specifier_pole_expertise",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exposer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    id_thematique = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exposer", x => new { x.id_pole_expertise, x.id_thematique });
                    table.ForeignKey(
                        name: "fk_pole_expertise_exposer",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_thematique_exposer",
                        column: x => x.id_thematique,
                        principalSchema: "usmbTech",
                        principalTable: "thematique",
                        principalColumn: "id_thematique",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consommable",
                schema: "usmbTech",
                columns: table => new
                {
                    id_consommable = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_unite = table.Column<int>(type: "integer", nullable: false),
                    nom_consommable = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    prix_unite = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    prix_forfait = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    forfait = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consommable", x => x.id_consommable);
                    table.ForeignKey(
                        name: "fk_unite_consommable",
                        column: x => x.id_unite,
                        principalSchema: "usmbTech",
                        principalTable: "unite",
                        principalColumn: "id_unite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact_usmb",
                schema: "usmbTech",
                columns: table => new
                {
                    id_Contact = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_fonction = table.Column<int>(type: "integer", nullable: false),
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    code_rh = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    num_securite_social = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    nom_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prenom_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    telephone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact_USMB", x => x.id_Contact);
                    table.ForeignKey(
                        name: "fk_fonction_contact",
                        column: x => x.id_fonction,
                        principalSchema: "usmbTech",
                        principalTable: "fonction",
                        principalColumn: "id_fonction",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_laboratoire_contact_USMB",
                        column: x => x.nom_court,
                        principalSchema: "usmbTech",
                        principalTable: "laboratoire",
                        principalColumn: "nom_court",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_mot_clef = table.Column<int>(type: "integer", nullable: false),
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_designer", x => new { x.id_mot_clef, x.nom_court });
                    table.ForeignKey(
                        name: "fk_laboratoire_designer",
                        column: x => x.nom_court,
                        principalSchema: "usmbTech",
                        principalTable: "laboratoire",
                        principalColumn: "nom_court",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mot_clef_designer",
                        column: x => x.id_mot_clef,
                        principalSchema: "usmbTech",
                        principalTable: "mot_clef",
                        principalColumn: "id_mot_clef",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "est_lier",
                schema: "usmbTech",
                columns: table => new
                {
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    id_thematique = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_est_lier", x => new { x.nom_court, x.id_thematique });
                    table.ForeignKey(
                        name: "fk_laboratoire_est_lier",
                        column: x => x.nom_court,
                        principalSchema: "usmbTech",
                        principalTable: "laboratoire",
                        principalColumn: "nom_court",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_thematique_est_lier",
                        column: x => x.id_thematique,
                        principalSchema: "usmbTech",
                        principalTable: "thematique",
                        principalColumn: "id_thematique",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gerer",
                schema: "usmbTech",
                columns: table => new
                {
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    pourcentage = table.Column<double>(type: "double precision", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gerer", x => new { x.id_pole_expertise, x.nom_court });
                    table.ForeignKey(
                        name: "fk_laboratoire_gerer",
                        column: x => x.nom_court,
                        principalSchema: "usmbTech",
                        principalTable: "laboratoire",
                        principalColumn: "nom_court",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pole_expertise_gerer",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "equipement",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    id_modele = table.Column<int>(type: "integer", nullable: false),
                    id_type_equipement = table.Column<int>(type: "integer", nullable: false),
                    nom_equipement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    num_immobilisation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    date_acquisition = table.Column<DateTime>(type: "date", nullable: false),
                    prix_achat = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    prix_revient = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    description_technique = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    disponibilite = table.Column<bool>(type: "boolean", nullable: false),
                    autonomie = table.Column<bool>(type: "boolean", nullable: false),
                    utilisable_chez_le_client = table.Column<bool>(type: "boolean", nullable: false),
                    actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipement", x => x.id_equipement);
                    table.ForeignKey(
                        name: "fk_modele_equipement",
                        column: x => x.id_modele,
                        principalSchema: "usmbTech",
                        principalTable: "modele",
                        principalColumn: "id_modele",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pole_expertise_equipement",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_equipement_equipement",
                        column: x => x.id_type_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "type_equipement",
                        principalColumn: "id_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "associer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    id_contact = table.Column<int>(type: "integer", nullable: false),
                    fonction = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_associer", x => new { x.id_contact, x.id_pole_expertise });
                    table.ForeignKey(
                        name: "fk_contact_USMB_associer",
                        column: x => x.id_contact,
                        principalSchema: "usmbTech",
                        principalTable: "contact_usmb",
                        principalColumn: "id_Contact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pole_expertise_associer",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prestation",
                schema: "usmbTech",
                columns: table => new
                {
                    id_prestation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_unite_oeuvre = table.Column<int>(type: "integer", nullable: false),
                    id_type_prestation = table.Column<int>(type: "integer", nullable: false),
                    nom_court = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    id_contact = table.Column<int>(type: "integer", nullable: false),
                    intitule_prestation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description_prestation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    prix_revient = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    prix_vente = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    peux_ce_realiser_chez_le_client = table.Column<bool>(type: "boolean", nullable: false),
                    actif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prestation", x => x.id_prestation);
                    table.ForeignKey(
                        name: "fk_prestation_contact_usmb",
                        column: x => x.id_contact,
                        principalSchema: "usmbTech",
                        principalTable: "contact_usmb",
                        principalColumn: "id_Contact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prestation_laboratoire",
                        column: x => x.nom_court,
                        principalSchema: "usmbTech",
                        principalTable: "laboratoire",
                        principalColumn: "nom_court",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_prestation_prestation",
                        column: x => x.id_type_prestation,
                        principalSchema: "usmbTech",
                        principalTable: "type_prestation",
                        principalColumn: "id_type_prestation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_unite_oeuvre_prestation",
                        column: x => x.id_unite_oeuvre,
                        principalSchema: "usmbTech",
                        principalTable: "unite_oeuvre",
                        principalColumn: "id_unite_oeuvre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consommer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    id_consommable = table.Column<int>(type: "integer", nullable: false),
                    quantite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consommer", x => new { x.id_equipement, x.id_consommable });
                    table.ForeignKey(
                        name: "fk_consommer_consommable",
                        column: x => x.id_consommable,
                        principalSchema: "usmbTech",
                        principalTable: "consommable",
                        principalColumn: "id_consommable",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_equipement_consommer",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exemple_utilisation",
                schema: "usmbTech",
                columns: table => new
                {
                    id_exemple_utilisation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_equipement = table.Column<int>(type: "integer", nullable: true),
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: true),
                    nom_utilisation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description_utilisation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exemple_utilisation", x => x.id_exemple_utilisation);
                    table.ForeignKey(
                        name: "fk_exemple_utilisation_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pole_expertise_exemple_utilisation",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photo",
                schema: "usmbTech",
                columns: table => new
                {
                    id_photo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_equipement = table.Column<int>(type: "integer", nullable: true),
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: true),
                    nom_photo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    url_photo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo", x => x.id_photo);
                    table.CheckConstraint("CK_Photo_EquipementOuPole_Expertise", "(\"id_equipement\" IS NULL) <> (\"id_pole_expertise\" IS NULL)");
                    table.ForeignKey(
                        name: "fk_photo_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pole_expertise_photo",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posseder",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    id_fonctionalite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posseder", x => new { x.id_equipement, x.id_fonctionalite });
                    table.ForeignKey(
                        name: "fk_posseder_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_posseder_fonctionalite",
                        column: x => x.id_fonctionalite,
                        principalSchema: "usmbTech",
                        principalTable: "fonctionalite",
                        principalColumn: "id_fonctionalite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prise_contact",
                schema: "usmbTech",
                columns: table => new
                {
                    num_prise_contact = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_equipement = table.Column<int>(type: "integer", nullable: true),
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: true),
                    id_type_client = table.Column<int>(type: "integer", nullable: false),
                    nom_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    prenom_contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    entreprise_contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email_contact = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    description_besoins = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prise_contact", x => x.num_prise_contact);
                    table.CheckConstraint("CK_PriseContact_EquipementOuPole_Expertise", "(\"id_equipement\" IS NULL) <> (\"id_pole_expertise\" IS NULL)");
                    table.ForeignKey(
                        name: "fk_prise_contact_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_prise_contact_pole_expertise",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_client_prise_contact",
                        column: x => x.id_type_client,
                        principalSchema: "usmbTech",
                        principalTable: "type_client",
                        principalColumn: "id_type_client",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "referencer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_contact = table.Column<int>(type: "integer", nullable: false),
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_referencer", x => new { x.id_equipement, x.id_contact });
                    table.ForeignKey(
                        name: "fk_referencer_contact",
                        column: x => x.id_contact,
                        principalSchema: "usmbTech",
                        principalTable: "contact_usmb",
                        principalColumn: "id_Contact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_referencer_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fournir",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    id_prestation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fournir", x => new { x.id_equipement, x.id_prestation });
                    table.ForeignKey(
                        name: "fk_fournir_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fournir_prestation",
                        column: x => x.id_prestation,
                        principalSchema: "usmbTech",
                        principalTable: "prestation",
                        principalColumn: "id_prestation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "presenter",
                schema: "usmbTech",
                columns: table => new
                {
                    id_pole_expertise = table.Column<int>(type: "integer", nullable: false),
                    id_prestation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_presenter", x => new { x.id_pole_expertise, x.id_prestation });
                    table.ForeignKey(
                        name: "fk_presenter_pole_expertise",
                        column: x => x.id_pole_expertise,
                        principalSchema: "usmbTech",
                        principalTable: "pole_expertise",
                        principalColumn: "id_pole_expertise",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_presenter_prestation",
                        column: x => x.id_prestation,
                        principalSchema: "usmbTech",
                        principalTable: "prestation",
                        principalColumn: "id_prestation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_associer_id_pole_expertise",
                schema: "usmbTech",
                table: "associer",
                column: "id_pole_expertise");

            migrationBuilder.CreateIndex(
                name: "IX_consommable_id_unite",
                schema: "usmbTech",
                table: "consommable",
                column: "id_unite");

            migrationBuilder.CreateIndex(
                name: "IX_consommer_id_consommable",
                schema: "usmbTech",
                table: "consommer",
                column: "id_consommable");

            migrationBuilder.CreateIndex(
                name: "IX_contact_usmb_id_fonction",
                schema: "usmbTech",
                table: "contact_usmb",
                column: "id_fonction");

            migrationBuilder.CreateIndex(
                name: "IX_contact_usmb_nom_court",
                schema: "usmbTech",
                table: "contact_usmb",
                column: "nom_court");

            migrationBuilder.CreateIndex(
                name: "IX_designer_nom_court",
                schema: "usmbTech",
                table: "designer",
                column: "nom_court");

            migrationBuilder.CreateIndex(
                name: "IX_equipement_id_modele",
                schema: "usmbTech",
                table: "equipement",
                column: "id_modele");

            migrationBuilder.CreateIndex(
                name: "IX_equipement_id_pole_expertise",
                schema: "usmbTech",
                table: "equipement",
                column: "id_pole_expertise");

            migrationBuilder.CreateIndex(
                name: "IX_equipement_id_type_equipement",
                schema: "usmbTech",
                table: "equipement",
                column: "id_type_equipement");

            migrationBuilder.CreateIndex(
                name: "IX_est_lier_id_thematique",
                schema: "usmbTech",
                table: "est_lier",
                column: "id_thematique");

            migrationBuilder.CreateIndex(
                name: "IX_exemple_utilisation_id_equipement",
                schema: "usmbTech",
                table: "exemple_utilisation",
                column: "id_equipement");

            migrationBuilder.CreateIndex(
                name: "IX_exemple_utilisation_id_pole_expertise",
                schema: "usmbTech",
                table: "exemple_utilisation",
                column: "id_pole_expertise");

            migrationBuilder.CreateIndex(
                name: "IX_exposer_id_thematique",
                schema: "usmbTech",
                table: "exposer",
                column: "id_thematique");

            migrationBuilder.CreateIndex(
                name: "IX_fournir_id_prestation",
                schema: "usmbTech",
                table: "fournir",
                column: "id_prestation");

            migrationBuilder.CreateIndex(
                name: "IX_gerer_nom_court",
                schema: "usmbTech",
                table: "gerer",
                column: "nom_court");

            migrationBuilder.CreateIndex(
                name: "IX_laboratoire_id_adresse_campus",
                schema: "usmbTech",
                table: "laboratoire",
                column: "id_adresse_campus");

            migrationBuilder.CreateIndex(
                name: "IX_laboratoire_id_adresse_labo",
                schema: "usmbTech",
                table: "laboratoire",
                column: "id_adresse_labo");

            migrationBuilder.CreateIndex(
                name: "IX_modele_id_marque",
                schema: "usmbTech",
                table: "modele",
                column: "id_marque");

            migrationBuilder.CreateIndex(
                name: "IX_photo_id_equipement",
                schema: "usmbTech",
                table: "photo",
                column: "id_equipement");

            migrationBuilder.CreateIndex(
                name: "IX_photo_id_pole_expertise",
                schema: "usmbTech",
                table: "photo",
                column: "id_pole_expertise");

            migrationBuilder.CreateIndex(
                name: "IX_posseder_id_fonctionalite",
                schema: "usmbTech",
                table: "posseder",
                column: "id_fonctionalite");

            migrationBuilder.CreateIndex(
                name: "IX_presenter_id_prestation",
                schema: "usmbTech",
                table: "presenter",
                column: "id_prestation");

            migrationBuilder.CreateIndex(
                name: "IX_prestation_id_contact",
                schema: "usmbTech",
                table: "prestation",
                column: "id_contact");

            migrationBuilder.CreateIndex(
                name: "IX_prestation_id_type_prestation",
                schema: "usmbTech",
                table: "prestation",
                column: "id_type_prestation");

            migrationBuilder.CreateIndex(
                name: "IX_prestation_id_unite_oeuvre",
                schema: "usmbTech",
                table: "prestation",
                column: "id_unite_oeuvre");

            migrationBuilder.CreateIndex(
                name: "IX_prestation_nom_court",
                schema: "usmbTech",
                table: "prestation",
                column: "nom_court");

            migrationBuilder.CreateIndex(
                name: "IX_prise_contact_id_equipement",
                schema: "usmbTech",
                table: "prise_contact",
                column: "id_equipement");

            migrationBuilder.CreateIndex(
                name: "IX_prise_contact_id_pole_expertise",
                schema: "usmbTech",
                table: "prise_contact",
                column: "id_pole_expertise");

            migrationBuilder.CreateIndex(
                name: "IX_prise_contact_id_type_client",
                schema: "usmbTech",
                table: "prise_contact",
                column: "id_type_client");

            migrationBuilder.CreateIndex(
                name: "IX_referencer_id_contact",
                schema: "usmbTech",
                table: "referencer",
                column: "id_contact");

            migrationBuilder.CreateIndex(
                name: "IX_specifier_id_pole_expertise",
                schema: "usmbTech",
                table: "specifier",
                column: "id_pole_expertise");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "associer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "consommer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "designer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "est_lier",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "exemple_utilisation",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "exposer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "fournir",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "gerer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "photo",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "posseder",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "presenter",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "prise_contact",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "referencer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "specifier",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "consommable",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "thematique",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "fonctionalite",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "prestation",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "type_client",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "equipement",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "mot_clef",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "unite",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "contact_usmb",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "type_prestation",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "unite_oeuvre",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "modele",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "pole_expertise",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "type_equipement",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "fonction",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "laboratoire",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "marque",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "adresse",
                schema: "usmbTech");
        }
    }
}
