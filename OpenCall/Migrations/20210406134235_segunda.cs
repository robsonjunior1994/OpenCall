using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenCall.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuario_UserId",
                table: "Chamados");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_UserId",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chamados");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Chamados");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Chamados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_UserId",
                table: "Chamados",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuario_UserId",
                table: "Chamados",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
