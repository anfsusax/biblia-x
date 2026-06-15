using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConhecimentoBiblico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCapitulo4ERelevancia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Relevancia",
                table: "ConexoesBiblicas",
                type: "int",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-f111-f111-f111-f11111111111"),
                column: "Relevancia",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-f222-f222-f222-f22222222222"),
                column: "Relevancia",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f3333333-f333-f333-f333-f33333333333"),
                column: "Relevancia",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f4444444-f444-f444-f444-f44444444444"),
                column: "Relevancia",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f5555555-f555-f555-f555-f55555555555"),
                column: "Relevancia",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("f6666666-f666-f666-f666-f66666666666"),
                column: "Relevancia",
                value: 7);

            migrationBuilder.InsertData(
                table: "ElementosBiblicos",
                columns: new[] { "Id", "Descricao", "Nome", "Ocupacao", "PeriodoHistorico", "TipoElemento" },
                values: new object[] { new Guid("88888888-8888-8888-8888-888888888888"), "Profeta que preparou o caminho do Senhor e batizou Jesus no Rio Jordão (Mateus 3).", "João Batista", "Profeta, Precursor", "Século I d.C.", 1 });

            migrationBuilder.InsertData(
                table: "ElementosBiblicos",
                columns: new[] { "Id", "Descricao", "ElementoCentral", "Nome", "TipoElemento" },
                values: new object[,]
                {
                    { new Guid("e0400001-e040-e040-e040-e04000000001"), "Jesus é batizado por João Batista no Rio Jordão. O Espírito Santo desce sobre Ele como pomba e o Pai declara: 'Este é o meu Filho amado' (Mateus 3.13-17).", true, "Batismo de Jesus", 3 },
                    { new Guid("e0400002-e040-e040-e040-e04000000002"), "Após o batismo, Jesus é levado pelo Espírito ao deserto onde permanece 40 dias e é tentado pelo diabo. Vence cada tentação com as Escrituras (Mateus 4.1-11).", true, "Tentação no Deserto", 3 }
                });

            migrationBuilder.InsertData(
                table: "ConexoesBiblicas",
                columns: new[] { "Id", "DestinoId", "Explicacao", "OrigemId", "Relevancia", "TipoConexao" },
                values: new object[,]
                {
                    { new Guid("cb400001-cb40-cb40-cb40-cb4000000001"), new Guid("11111111-1111-1111-1111-111111111111"), "Jesus é o protagonista do batismo. É sobre Ele que o Espírito desce e o Pai fala (Mateus 3.16-17).", new Guid("e0400001-e040-e040-e040-e04000000001"), 10, 13 },
                    { new Guid("cb400002-cb40-cb40-cb40-cb4000000002"), new Guid("88888888-8888-8888-8888-888888888888"), "João Batista administra o batismo de Jesus, cumprindo seu papel de precursor e preparador do caminho (Mateus 3.13-15).", new Guid("e0400001-e040-e040-e040-e04000000001"), 8, 13 },
                    { new Guid("cb400003-cb40-cb40-cb40-cb4000000003"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "O batismo de Jesus é ato de fé e obediência — cumprimento de toda a justiça (Mateus 3.15).", new Guid("e0400001-e040-e040-e040-e04000000001"), 7, 12 },
                    { new Guid("cb400004-cb40-cb40-cb40-cb4000000004"), new Guid("11111111-1111-1111-1111-111111111111"), "Jesus é levado ao deserto pelo Espírito e enfrenta pessoalmente as tentações do diabo por 40 dias (Mateus 4.1).", new Guid("e0400002-e040-e040-e040-e04000000002"), 10, 13 },
                    { new Guid("cb400005-cb40-cb40-cb40-cb4000000005"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "A tentação no deserto demonstra que Jesus venceu pela fé na Palavra de Deus, citando as Escrituras em cada resposta (Mateus 4.4, 4.7, 4.10).", new Guid("e0400002-e040-e040-e040-e04000000002"), 8, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("cb400001-cb40-cb40-cb40-cb4000000001"));

            migrationBuilder.DeleteData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("cb400002-cb40-cb40-cb40-cb4000000002"));

            migrationBuilder.DeleteData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("cb400003-cb40-cb40-cb40-cb4000000003"));

            migrationBuilder.DeleteData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("cb400004-cb40-cb40-cb40-cb4000000004"));

            migrationBuilder.DeleteData(
                table: "ConexoesBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("cb400005-cb40-cb40-cb40-cb4000000005"));

            migrationBuilder.DeleteData(
                table: "ElementosBiblicos",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "ElementosBiblicos",
                keyColumn: "Id",
                keyValue: new Guid("e0400001-e040-e040-e040-e04000000001"));

            migrationBuilder.DeleteData(
                table: "ElementosBiblicos",
                keyColumn: "Id",
                keyValue: new Guid("e0400002-e040-e040-e040-e04000000002"));

            migrationBuilder.DropColumn(
                name: "Relevancia",
                table: "ConexoesBiblicas");
        }
    }
}
