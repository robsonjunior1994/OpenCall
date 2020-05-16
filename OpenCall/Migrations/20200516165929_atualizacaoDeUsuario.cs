using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenCall.Migrations
{
    public partial class atualizacaoDeUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Usuario",
                newName: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Usuario",
                newName: "Token");
        }
    }
}
