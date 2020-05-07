using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenCall.Migrations
{
    public partial class correcaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuario_userId",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Chamados",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chamados_userId",
                table: "Chamados",
                newName: "IX_Chamados_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuario_UserId",
                table: "Chamados",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuario_UserId",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chamados",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Chamados_UserId",
                table: "Chamados",
                newName: "IX_Chamados_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuario_userId",
                table: "Chamados",
                column: "userId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
