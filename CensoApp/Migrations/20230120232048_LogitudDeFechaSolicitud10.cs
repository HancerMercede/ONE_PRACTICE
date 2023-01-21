using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoApp.Migrations
{
    public partial class LogitudDeFechaSolicitud10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProvincia",
                table: "Municipios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProvincia",
                table: "Municipios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
