using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class DepartamentoForeignKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador");

            migrationBuilder.DropColumn(
                name: "DepartmentoId",
                table: "Operador");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Operador",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Operador",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentoId",
                table: "Operador",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Operador_Departamento_DepartamentoId",
                table: "Operador",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
