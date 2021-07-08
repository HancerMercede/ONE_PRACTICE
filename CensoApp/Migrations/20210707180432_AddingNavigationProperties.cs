using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoApp.Migrations
{
    public partial class AddingNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipios_Provincias_IdProvincia",
                table: "Municipios");

            migrationBuilder.DropForeignKey(
                name: "FK_Provincias_Municipios_MunicipioId",
                table: "Provincias");

            migrationBuilder.DropIndex(
                name: "IX_Provincias_MunicipioId",
                table: "Provincias");

            migrationBuilder.DropIndex(
                name: "IX_Municipios_IdProvincia",
                table: "Municipios");

            migrationBuilder.DropColumn(
                name: "IdMunicipio",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "Provincias");

            migrationBuilder.AddColumn<int>(
                name: "ProvinciaId",
                table: "Municipios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipios_Provincias_ProvinciaId",
                table: "Municipios");

            migrationBuilder.DropIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios");

            migrationBuilder.DropColumn(
                name: "ProvinciaId",
                table: "Municipios");

            migrationBuilder.AddColumn<int>(
                name: "IdMunicipio",
                table: "Provincias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioId",
                table: "Provincias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_MunicipioId",
                table: "Provincias",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_IdProvincia",
                table: "Municipios",
                column: "IdProvincia");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipios_Provincias_IdProvincia",
                table: "Municipios",
                column: "IdProvincia",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provincias_Municipios_MunicipioId",
                table: "Provincias",
                column: "MunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
