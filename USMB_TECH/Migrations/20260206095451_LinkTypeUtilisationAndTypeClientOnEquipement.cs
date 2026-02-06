using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class LinkTypeUtilisationAndTypeClientOnEquipement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_autoriser_equipement",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.DropForeignKey(
                name: "fk_autoriser_type_client",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.DropForeignKey(
                name: "fk_proposer_equipement",
                schema: "usmbTech",
                table: "proposer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_proposer",
                schema: "usmbTech",
                table: "proposer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_autoriser",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_proposer",
                schema: "usmbTech",
                table: "proposer",
                columns: new[] { "id_equipement", "id_type_utilisation" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_autoriser",
                schema: "usmbTech",
                table: "autoriser",
                columns: new[] { "id_equipement", "id_type_client" });

            migrationBuilder.AddForeignKey(
                name: "FK_autoriser_equipement_id_equipement",
                schema: "usmbTech",
                table: "autoriser",
                column: "id_equipement",
                principalSchema: "usmbTech",
                principalTable: "equipement",
                principalColumn: "id_equipement",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_autoriser_type_client_id_type_client",
                schema: "usmbTech",
                table: "autoriser",
                column: "id_type_client",
                principalSchema: "usmbTech",
                principalTable: "type_client",
                principalColumn: "id_type_client",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_proposer_equipement_id_equipement",
                schema: "usmbTech",
                table: "proposer",
                column: "id_equipement",
                principalSchema: "usmbTech",
                principalTable: "equipement",
                principalColumn: "id_equipement",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_autoriser_equipement_id_equipement",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.DropForeignKey(
                name: "FK_autoriser_type_client_id_type_client",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.DropForeignKey(
                name: "FK_proposer_equipement_id_equipement",
                schema: "usmbTech",
                table: "proposer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_proposer",
                schema: "usmbTech",
                table: "proposer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_autoriser",
                schema: "usmbTech",
                table: "autoriser");

            migrationBuilder.AddPrimaryKey(
                name: "pk_proposer",
                schema: "usmbTech",
                table: "proposer",
                columns: new[] { "id_equipement", "id_type_utilisation" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_autoriser",
                schema: "usmbTech",
                table: "autoriser",
                columns: new[] { "id_equipement", "id_type_client" });

            migrationBuilder.AddForeignKey(
                name: "fk_autoriser_equipement",
                schema: "usmbTech",
                table: "autoriser",
                column: "id_equipement",
                principalSchema: "usmbTech",
                principalTable: "equipement",
                principalColumn: "id_equipement",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_autoriser_type_client",
                schema: "usmbTech",
                table: "autoriser",
                column: "id_type_client",
                principalSchema: "usmbTech",
                principalTable: "type_client",
                principalColumn: "id_type_client",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proposer_equipement",
                schema: "usmbTech",
                table: "proposer",
                column: "id_equipement",
                principalSchema: "usmbTech",
                principalTable: "equipement",
                principalColumn: "id_equipement",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
