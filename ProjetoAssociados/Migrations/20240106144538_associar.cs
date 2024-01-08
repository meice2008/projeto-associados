using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoAssociados.Migrations
{
    public partial class associar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociadosEmpresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssociadoId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociadosEmpresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociadosEmpresa_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociadosEmpresa_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosEmpresa_AssociadoId",
                table: "AssociadosEmpresa",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosEmpresa_EmpresaId",
                table: "AssociadosEmpresa",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociadosEmpresa");
        }
    }
}
