using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class CriandoTabelaCliente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operador",
                table: "Fechamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operador",
                table: "Fechamento",
                nullable: false,
                defaultValue: "");
        }
    }
}
