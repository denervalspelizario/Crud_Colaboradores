using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Motel_Luxor.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelaDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Colaboradores",
                table: "Colaboradores");

            migrationBuilder.RenameTable(
                name: "Colaboradores",
                newName: "colaboradores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_nascimento",
                table: "colaboradores",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_admissao",
                table: "colaboradores",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_colaboradores",
                table: "colaboradores",
                column: "colaborador_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_colaboradores",
                table: "colaboradores");

            migrationBuilder.RenameTable(
                name: "colaboradores",
                newName: "Colaboradores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_nascimento",
                table: "Colaboradores",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_admissao",
                table: "Colaboradores",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colaboradores",
                table: "Colaboradores",
                column: "colaborador_id");
        }
    }
}
