using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class Table_LogMovimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_AUDIT_Produtos_ProdutoId",
                table: "Produtos_AUDIT");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Produtos_AUDIT",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LogMovimentos",
                columns: table => new
                {
                    MovId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextoMovimento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogMovimentos", x => x.MovId);
                    table.ForeignKey(
                        name: "FK_LogMovimentos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogMovimentos_ProdutoId",
                table: "LogMovimentos",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_AUDIT_Produtos_ProdutoId",
                table: "Produtos_AUDIT",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_AUDIT_Produtos_ProdutoId",
                table: "Produtos_AUDIT");

            migrationBuilder.DropTable(
                name: "LogMovimentos");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Produtos_AUDIT",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_AUDIT_Produtos_ProdutoId",
                table: "Produtos_AUDIT",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");
        }
    }
}
