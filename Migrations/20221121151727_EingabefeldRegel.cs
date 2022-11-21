using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gandalan.IDAS.IDASWebApp.Migrations
{
    /// <inheritdoc />
    public partial class EingabefeldRegel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Regel",
                table: "EingabeFelder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regel",
                table: "EingabeFelder");
        }
    }
}
