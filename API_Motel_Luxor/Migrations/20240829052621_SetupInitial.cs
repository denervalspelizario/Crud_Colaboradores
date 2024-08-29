using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Motel_Luxor.Migrations
{
    /// <inheritdoc />
    public partial class SetupInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    colaborador_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sexo = table.Column<int>(type: "int", nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_admissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cargo = table.Column<int>(type: "int", nullable: false),
                    salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    departamento = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.colaborador_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaboradores");
        }
    }
}
