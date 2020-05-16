using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenCall.Migrations
{
    public partial class AtualizandoNomeDataKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataToken",
                table: "Usuario",
                newName: "DataKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataKey",
                table: "Usuario",
                newName: "DataToken");
        }
    }
}
