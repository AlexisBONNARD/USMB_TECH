using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class AddAutoriserProposerTypeUtilisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nom_typeclient",
                schema: "usmbTech",
                table: "type_client",
                newName: "nom_type_client");

            migrationBuilder.AddColumn<double>(
                name: "mult_tarif_type_client",
                schema: "usmbTech",
                table: "type_client",
                type: "double precision",
                precision: 2,
                scale: 2,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "autoriser",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    id_type_client = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_autoriser", x => new { x.id_equipement, x.id_type_client });
                    table.ForeignKey(
                        name: "fk_autoriser_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_autoriser_type_client",
                        column: x => x.id_type_client,
                        principalSchema: "usmbTech",
                        principalTable: "type_client",
                        principalColumn: "id_type_client",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_utilisation",
                schema: "usmbTech",
                columns: table => new
                {
                    id_type_utilisation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_type_utilisation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_utilisation", x => x.id_type_utilisation);
                });

            migrationBuilder.CreateTable(
                name: "proposer",
                schema: "usmbTech",
                columns: table => new
                {
                    id_equipement = table.Column<int>(type: "integer", nullable: false),
                    id_type_utilisation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proposer", x => new { x.id_equipement, x.id_type_utilisation });
                    table.ForeignKey(
                        name: "fk_proposer_equipement",
                        column: x => x.id_equipement,
                        principalSchema: "usmbTech",
                        principalTable: "equipement",
                        principalColumn: "id_equipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_utilisation_proposer",
                        column: x => x.id_type_utilisation,
                        principalSchema: "usmbTech",
                        principalTable: "type_utilisation",
                        principalColumn: "id_type_utilisation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autoriser_id_type_client",
                schema: "usmbTech",
                table: "autoriser",
                column: "id_type_client");

            migrationBuilder.CreateIndex(
                name: "IX_proposer_id_type_utilisation",
                schema: "usmbTech",
                table: "proposer",
                column: "id_type_utilisation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autoriser",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "proposer",
                schema: "usmbTech");

            migrationBuilder.DropTable(
                name: "type_utilisation",
                schema: "usmbTech");

            migrationBuilder.DropColumn(
                name: "mult_tarif_type_client",
                schema: "usmbTech",
                table: "type_client");

            migrationBuilder.RenameColumn(
                name: "nom_type_client",
                schema: "usmbTech",
                table: "type_client",
                newName: "nom_typeclient");
        }
    }
}
