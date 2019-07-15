using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class FechamentoForeignKeyOperadorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Operador",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Operador",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OperadorId",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento",
                column: "OperadorId",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Operador",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Operador",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "OperadorId",
                table: "Fechamento",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Fechamento_Operador_OperadorId",
                table: "Fechamento",
                column: "OperadorId",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
