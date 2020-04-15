using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class CriarTabelaClientNoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Fechamento",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    TipoRegistroNacional = table.Column<int>(nullable: false),
                    RegistroNacional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fechamento_ClientId",
                table: "Fechamento",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fechamento_Client_ClientId",
                table: "Fechamento",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fechamento_Client_ClientId",
                table: "Fechamento");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Fechamento_ClientId",
                table: "Fechamento");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Fechamento");
        }
    }
}
