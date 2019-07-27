using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class ExcludeFKOperadorXTipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Tipo_TipoId",
                table: "Operador");

            migrationBuilder.DropIndex(
                name: "IX_Operador_TipoId",
                table: "Operador");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Operador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Operador",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Operador_TipoId",
                table: "Operador",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operador_Tipo_TipoId",
                table: "Operador",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
