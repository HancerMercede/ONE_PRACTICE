using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoApp.Migrations
{
    public partial class FijandoADateLaFechaSolicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Participantes",
                type: "date",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Participantes",
                type: "datetime2",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldMaxLength: 10);
        }
    }
}
