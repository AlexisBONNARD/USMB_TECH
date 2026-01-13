using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class EnleverContenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description_contenu",
                schema: "usmbTech",
                table: "pole_expertise");

            migrationBuilder.DropColumn(
                name: "nom_contenu",
                schema: "usmbTech",
                table: "pole_expertise");

            migrationBuilder.DropColumn(
                name: "url_contenu",
                schema: "usmbTech",
                table: "pole_expertise");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description_contenu",
                schema: "usmbTech",
                table: "pole_expertise",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nom_contenu",
                schema: "usmbTech",
                table: "pole_expertise",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "url_contenu",
                schema: "usmbTech",
                table: "pole_expertise",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
