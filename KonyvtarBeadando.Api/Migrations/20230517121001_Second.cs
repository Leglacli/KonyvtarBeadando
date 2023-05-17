using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonyvtarSzerver.Api.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kolcsonzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlvasoSzam = table.Column<int>(type: "int", nullable: false),
                    LeltariSzam = table.Column<int>(type: "int", nullable: false),
                    KolcsonzesIdeje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisszahozasIdeje = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kolcsonzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lakcim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlvasoSzam = table.Column<int>(type: "int", nullable: false),
                    SzuletesiDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kolcsonzes");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
