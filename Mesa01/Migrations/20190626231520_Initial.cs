using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fechamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(nullable: true),
                    Operador = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Empresa = table.Column<string>(nullable: true),
                    Valor = table.Column<float>(nullable: false),
                    Taxa = table.Column<float>(nullable: false),
                    Despesa = table.Column<float>(nullable: false),
                    Fluxo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fechamento", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fechamento");
        }
    }
}
