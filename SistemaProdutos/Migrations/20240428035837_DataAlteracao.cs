using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class DataAlteracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Produtos_AUDIT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 28, 0, 58, 37, 325, DateTimeKind.Local).AddTicks(8782));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Produtos_AUDIT");
        }
    }
}
