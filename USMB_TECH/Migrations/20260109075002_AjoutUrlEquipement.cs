using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class AjoutUrlEquipement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url_modele_3d",
                schema: "usmbTech",
                table: "equipement",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url_modele_3d",
                schema: "usmbTech",
                table: "equipement");
        }
    }
}
