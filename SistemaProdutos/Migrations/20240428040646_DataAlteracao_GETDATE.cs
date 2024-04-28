using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProdutos.Migrations
{
    /// <inheritdoc />
    public partial class DataAlteracao_GETDATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAlteracao",
                table: "Produtos_AUDIT",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 28, 0, 58, 37, 325, DateTimeKind.Local).AddTicks(8782));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAlteracao",
                table: "Produtos_AUDIT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 28, 0, 58, 37, 325, DateTimeKind.Local).AddTicks(8782),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
