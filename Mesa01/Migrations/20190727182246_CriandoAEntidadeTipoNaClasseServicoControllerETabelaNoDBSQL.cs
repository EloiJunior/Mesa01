using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mesa01.Migrations
{
    public partial class CriandoAEntidadeTipoNaClasseServicoControllerETabelaNoDBSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Operador",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Empresa",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Banco",
                table: "Fechamento",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Fechamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tipo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operador_Tipo_TipoId",
                table: "Operador");

            migrationBuilder.DropTable(
                name: "Tipo");

            migrationBuilder.DropIndex(
                name: "IX_Operador_TipoId",
                table: "Operador");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Operador");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Fechamento");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Fechamento",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Empresa",
                table: "Fechamento",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Banco",
                table: "Fechamento",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
