﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoApp.Migrations
{
    public partial class AddRemoveProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Sector",
                table: "Participantes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
