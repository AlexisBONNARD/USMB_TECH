using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USMB_TECH.Migrations
{
    /// <inheritdoc />
    public partial class suppressionNumSecuSociale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "num_securite_social",
                schema: "usmbTech",
                table: "contact_usmb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "num_securite_social",
                schema: "usmbTech",
                table: "contact_usmb",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
