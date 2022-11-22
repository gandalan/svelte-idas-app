using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gandalan.IDAS.IDASWebApp.Migrations
{
    /// <inheritdoc />
    public partial class VarianteKonfigSatzUInullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Varianten_KonfigSaetze_KonfigSatzId",
                table: "Varianten");

            migrationBuilder.DropForeignKey(
                name: "FK_Varianten_UIDefinitionen_UIDefinitionId",
                table: "Varianten");

            migrationBuilder.AlterColumn<long>(
                name: "UIDefinitionId",
                table: "Varianten",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "KonfigSatzId",
                table: "Varianten",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Varianten_KonfigSaetze_KonfigSatzId",
                table: "Varianten",
                column: "KonfigSatzId",
                principalTable: "KonfigSaetze",
                principalColumn: "KonfigSatzId");

            migrationBuilder.AddForeignKey(
                name: "FK_Varianten_UIDefinitionen_UIDefinitionId",
                table: "Varianten",
                column: "UIDefinitionId",
                principalTable: "UIDefinitionen",
                principalColumn: "UIDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Varianten_KonfigSaetze_KonfigSatzId",
                table: "Varianten");

            migrationBuilder.DropForeignKey(
                name: "FK_Varianten_UIDefinitionen_UIDefinitionId",
                table: "Varianten");

            migrationBuilder.AlterColumn<long>(
                name: "UIDefinitionId",
                table: "Varianten",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "KonfigSatzId",
                table: "Varianten",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Varianten_KonfigSaetze_KonfigSatzId",
                table: "Varianten",
                column: "KonfigSatzId",
                principalTable: "KonfigSaetze",
                principalColumn: "KonfigSatzId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Varianten_UIDefinitionen_UIDefinitionId",
                table: "Varianten",
                column: "UIDefinitionId",
                principalTable: "UIDefinitionen",
                principalColumn: "UIDefinitionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
