using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class Log_movimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogMovimentos_Produtos_ProdutoId",
                table: "LogMovimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogMovimentos",
                table: "LogMovimentos");

            migrationBuilder.RenameTable(
                name: "LogMovimentos",
                newName: "Log_Movimentos");

            migrationBuilder.RenameIndex(
                name: "IX_LogMovimentos_ProdutoId",
                table: "Log_Movimentos",
                newName: "IX_Log_Movimentos_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log_Movimentos",
                table: "Log_Movimentos",
                column: "MovId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Movimentos_Produtos_ProdutoId",
                table: "Log_Movimentos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Movimentos_Produtos_ProdutoId",
                table: "Log_Movimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log_Movimentos",
                table: "Log_Movimentos");

            migrationBuilder.RenameTable(
                name: "Log_Movimentos",
                newName: "LogMovimentos");

            migrationBuilder.RenameIndex(
                name: "IX_Log_Movimentos_ProdutoId",
                table: "LogMovimentos",
                newName: "IX_LogMovimentos_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogMovimentos",
                table: "LogMovimentos",
                column: "MovId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogMovimentos_Produtos_ProdutoId",
                table: "LogMovimentos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
