using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class InserirFechamentoBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Banco",
                table: "Fechamento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banco",
                table: "Fechamento");
        }
    }
}
