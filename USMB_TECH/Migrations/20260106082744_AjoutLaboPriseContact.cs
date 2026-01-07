using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class AjoutLaboPriseContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id_sous_thematique",
                schema: "usmbTech",
                table: "thematique",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "nom_court",
                schema: "usmbTech",
                table: "prise_contact",
                type: "character varying(25)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prise_contact_nom_court",
                schema: "usmbTech",
                table: "prise_contact",
                column: "nom_court");

            migrationBuilder.AddForeignKey(
                name: "FK_prise_contact_laboratoire_nom_court",
                schema: "usmbTech",
                table: "prise_contact",
                column: "nom_court",
                principalSchema: "usmbTech",
                principalTable: "laboratoire",
                principalColumn: "nom_court");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prise_contact_laboratoire_nom_court",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.DropIndex(
                name: "IX_prise_contact_nom_court",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.DropColumn(
                name: "nom_court",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.AlterColumn<int>(
                name: "id_sous_thematique",
                schema: "usmbTech",
                table: "thematique",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
