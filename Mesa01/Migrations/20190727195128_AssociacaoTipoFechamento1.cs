using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class AssociacaoTipoFechamento1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fechamento_TipoId",
                table: "Fechamento",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fechamento_Tipo_TipoId",
                table: "Fechamento",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fechamento_Tipo_TipoId",
                table: "Fechamento");

            migrationBuilder.DropIndex(
                name: "IX_Fechamento_TipoId",
                table: "Fechamento");
        }
    }
}
