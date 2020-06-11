using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Personeelsdienst.Data.Migrations
{
    public partial class CreateDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entiteiten",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entiteitsnaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entiteiten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personeelsleden",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    EntiteitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeelsleden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personeelsleden_Entiteiten_EntiteitId",
                        column: x => x.EntiteitId,
                        principalTable: "Entiteiten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Afwezigheden",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersoneelslidId = table.Column<long>(nullable: false),
                    RedenAfwezigheid = table.Column<int>(nullable: false),
                    BeginDatum = table.Column<DateTime>(nullable: false),
                    EindDatum = table.Column<DateTime>(nullable: true),
                    Vervanger = table.Column<string>(nullable: true),
                    Verwerkt = table.Column<bool>(nullable: false),
                    BewijsOk = table.Column<bool>(nullable: false),
                    Bijlage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afwezigheden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Afwezigheden_Personeelsleden_PersoneelslidId",
                        column: x => x.PersoneelslidId,
                        principalTable: "Personeelsleden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afwezigheden_PersoneelslidId",
                table: "Afwezigheden",
                column: "PersoneelslidId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeelsleden_EntiteitId",
                table: "Personeelsleden",
                column: "EntiteitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Afwezigheden");

            migrationBuilder.DropTable(
                name: "Personeelsleden");

            migrationBuilder.DropTable(
                name: "Entiteiten");
        }
    }
}
