using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class ContraintePriseContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prise_contact_laboratoire_nom_court",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PriseContact_EquipementOuPole_Expertise",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PriseContact_Unique_Cible",
                schema: "usmbTech",
                table: "prise_contact",
                sql: "((\"id_equipement\" IS NOT NULL)::int + (\"id_pole_expertise\" IS NOT NULL)::int + (\"nom_court\" IS NOT NULL)::int) = 1");

            migrationBuilder.AddForeignKey(
                name: "fk_prise_contact_laboratoire",
                schema: "usmbTech",
                table: "prise_contact",
                column: "nom_court",
                principalSchema: "usmbTech",
                principalTable: "laboratoire",
                principalColumn: "nom_court",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_prise_contact_laboratoire",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PriseContact_Unique_Cible",
                schema: "usmbTech",
                table: "prise_contact");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PriseContact_EquipementOuPole_Expertise",
                schema: "usmbTech",
                table: "prise_contact",
                sql: "(\"id_equipement\" IS NULL) <> (\"id_pole_expertise\" IS NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_prise_contact_laboratoire_nom_court",
                schema: "usmbTech",
                table: "prise_contact",
                column: "nom_court",
                principalSchema: "usmbTech",
                principalTable: "laboratoire",
                principalColumn: "nom_court");
        }
    }
}
