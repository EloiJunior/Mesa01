using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class OtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operador",
                table: "Fechamento");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Taxa",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Despesa",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "OperadorId",
                table: "Fechamento",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Fechamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BaseSalary = table.Column<double>(nullable: false),
                    DepartamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operador_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fechamento_OperadorId",
                table: "Fechamento",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Operador_DepartamentoId",
                table: "Operador",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento",
                column: "OperadorId",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento");

            migrationBuilder.DropTable(
                name: "Operador");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Fechamento_OperadorId",
                table: "Fechamento");

            migrationBuilder.DropColumn(
                name: "OperadorId",
                table: "Fechamento");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fechamento");

            migrationBuilder.AlterColumn<float>(
                name: "Valor",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Taxa",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Despesa",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Operador",
                table: "Fechamento",
                nullable: true);
        }
    }
}
