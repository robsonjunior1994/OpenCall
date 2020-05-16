using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenCall.Migrations
{
    public partial class configurandoToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataToken",
                table: "Usuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataToken",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuario");
        }
    }
}
