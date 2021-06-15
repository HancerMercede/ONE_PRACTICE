using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoApp.Migrations
{
    public partial class AddInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    TipoCredencial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credencial = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Credencial",
                table: "Participantes",
                column: "Credencial",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participantes");
        }
    }
}
