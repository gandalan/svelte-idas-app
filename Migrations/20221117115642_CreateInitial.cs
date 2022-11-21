using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gandalan.IDAS.IDASWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KonfigSaetze",
                columns: table => new
                {
                    KonfigSatzId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KonfigSatzGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonfigSaetze", x => x.KonfigSatzId);
                });

            migrationBuilder.CreateTable(
                name: "UIDefinitionen",
                columns: table => new
                {
                    UIDefinitionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kategorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BezeichnungKurz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BezeichnungLang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BildHorizontal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BildVertikal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bild3D = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UIDefinitionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIDefinitionen", x => x.UIDefinitionId);
                });

            migrationBuilder.CreateTable(
                name: "WerteListe",
                columns: table => new
                {
                    WerteListeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WerteListeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerteListe", x => x.WerteListeId);
                });

            migrationBuilder.CreateTable(
                name: "KonfigSatzEintraege",
                columns: table => new
                {
                    KonfigSatzEintragId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KonfigSatzId = table.Column<long>(type: "bigint", nullable: false),
                    UnterkomponenteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KonfigName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatenTyp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KonfigSatzEintragGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonfigSatzEintraege", x => x.KonfigSatzEintragId);
                    table.ForeignKey(
                        name: "FK_KonfigSatzEintraege_KonfigSaetze_KonfigSatzId",
                        column: x => x.KonfigSatzId,
                        principalTable: "KonfigSaetze",
                        principalColumn: "KonfigSatzId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EingabeFelder",
                columns: table => new
                {
                    UIEingabeFeldId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UIDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    Reihenfolge = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regel = table.Column<int>(type: "int", nullable: false),
                    MinWert = table.Column<double>(type: "float", nullable: false),
                    MinWertWeichPruefen = table.Column<bool>(type: "bit", nullable: false),
                    MaxWert = table.Column<double>(type: "float", nullable: false),
                    MaxWertWeichPruefen = table.Column<bool>(type: "bit", nullable: false),
                    VorgabeWert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HilfeText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FehlerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarnText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BelegBlattText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AngebotsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WerteListeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreisFeldAnzeigen = table.Column<bool>(type: "bit", nullable: false),
                    MindestBreite = table.Column<int>(type: "int", nullable: false),
                    UIEingabeFeldGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EingabeLevel = table.Column<int>(type: "int", nullable: false),
                    ZusatzFeldGruppeId = table.Column<int>(type: "int", nullable: true),
                    GehoertZuZusatzFeldGruppeId = table.Column<int>(type: "int", nullable: true),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IstKonfiguratorFeld = table.Column<bool>(type: "bit", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EingabeFelder", x => x.UIEingabeFeldId);
                    table.ForeignKey(
                        name: "FK_EingabeFelder_UIDefinitionen_UIDefinitionId",
                        column: x => x.UIDefinitionId,
                        principalTable: "UIDefinitionen",
                        principalColumn: "UIDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonfiguratorFelder",
                columns: table => new
                {
                    UIKonfiguratorFeldId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UIDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    EingabeLevel = table.Column<int>(type: "int", nullable: false),
                    Reihenfolge = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kuerzel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WerteListeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VorgabeWert = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BelegBlattText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AngebotsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilId = table.Column<int>(type: "int", nullable: true),
                    GehoertZuProfilId = table.Column<int>(type: "int", nullable: true),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UIKonfiguratorFeldGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonfiguratorFelder", x => x.UIKonfiguratorFeldId);
                    table.ForeignKey(
                        name: "FK_KonfiguratorFelder_UIDefinitionen_UIDefinitionId",
                        column: x => x.UIDefinitionId,
                        principalTable: "UIDefinitionen",
                        principalColumn: "UIDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Varianten",
                columns: table => new
                {
                    VarianteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarianteGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KonfigSatzId = table.Column<long>(type: "bigint", nullable: false),
                    KonfigSatzGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UIDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    UIDefinitionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varianten", x => x.VarianteId);
                    table.ForeignKey(
                        name: "FK_Varianten_KonfigSaetze_KonfigSatzId",
                        column: x => x.KonfigSatzId,
                        principalTable: "KonfigSaetze",
                        principalColumn: "KonfigSatzId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Varianten_UIDefinitionen_UIDefinitionId",
                        column: x => x.UIDefinitionId,
                        principalTable: "UIDefinitionen",
                        principalColumn: "UIDefinitionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WerteListeItem",
                columns: table => new
                {
                    WerteListeItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WerteListeId = table.Column<long>(type: "bigint", nullable: false),
                    WerteListeItemGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reihenfolge = table.Column<int>(type: "int", nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kuerzel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BelegBlattText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AngebotsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GueltigAb = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GueltigBis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WerteListeItem", x => x.WerteListeItemId);
                    table.ForeignKey(
                        name: "FK_WerteListeItem_WerteListe_WerteListeId",
                        column: x => x.WerteListeId,
                        principalTable: "WerteListe",
                        principalColumn: "WerteListeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EingabeFelder_UIDefinitionId",
                table: "EingabeFelder",
                column: "UIDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_KonfigSatzEintraege_KonfigSatzId",
                table: "KonfigSatzEintraege",
                column: "KonfigSatzId");

            migrationBuilder.CreateIndex(
                name: "IX_KonfiguratorFelder_UIDefinitionId",
                table: "KonfiguratorFelder",
                column: "UIDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Varianten_KonfigSatzId",
                table: "Varianten",
                column: "KonfigSatzId");

            migrationBuilder.CreateIndex(
                name: "IX_Varianten_UIDefinitionId",
                table: "Varianten",
                column: "UIDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_WerteListeItem_WerteListeId",
                table: "WerteListeItem",
                column: "WerteListeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EingabeFelder");

            migrationBuilder.DropTable(
                name: "KonfigSatzEintraege");

            migrationBuilder.DropTable(
                name: "KonfiguratorFelder");

            migrationBuilder.DropTable(
                name: "Varianten");

            migrationBuilder.DropTable(
                name: "WerteListeItem");

            migrationBuilder.DropTable(
                name: "KonfigSaetze");

            migrationBuilder.DropTable(
                name: "UIDefinitionen");

            migrationBuilder.DropTable(
                name: "WerteListe");
        }
    }
}
