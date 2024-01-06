using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoAssociados.Migrations
{
    public partial class chaveUnica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Cnpj",
                table: "Empresas",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Associados_Cpf",
                table: "Associados",
                column: "Cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empresas_Cnpj",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Associados_Cpf",
                table: "Associados");
        }
    }
}
