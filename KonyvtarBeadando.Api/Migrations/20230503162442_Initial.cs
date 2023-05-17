using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonyvtarSzerver.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Konyv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Szerzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kiado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeltariSzam = table.Column<int>(type: "int", nullable: false),
                    KiadasEve = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konyv", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konyv");
        }
    }
}
