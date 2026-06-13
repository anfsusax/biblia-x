using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConhecimentoBiblico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElementosBiblicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TipoElemento = table.Column<int>(type: "int", nullable: false),
                    ElementoCentral = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Testamento = table.Column<int>(type: "int", nullable: true),
                    NumeroCaps = table.Column<int>(type: "int", nullable: true),
                    Regiao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LicaoCentral = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Livro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Capitulo = table.Column<int>(type: "int", nullable: true),
                    VersiculoInicial = table.Column<int>(type: "int", nullable: true),
                    VersiculoFinal = table.Column<int>(type: "int", nullable: true),
                    TextoResumo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PeriodoHistorico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ocupacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Cumprida = table.Column<bool>(type: "bit", nullable: true),
                    TextoCumprimento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementosBiblicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerguntasBiblicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pergunta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntasBiblicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConexoesBiblicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoConexao = table.Column<int>(type: "int", nullable: false),
                    Explicacao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConexoesBiblicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConexoesBiblicas_ElementosBiblicos_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "ElementosBiblicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConexoesBiblicas_ElementosBiblicos_OrigemId",
                        column: x => x.OrigemId,
                        principalTable: "ElementosBiblicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerguntaElementos",
                columns: table => new
                {
                    PerguntaBiblicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementoBiblicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaElementos", x => new { x.PerguntaBiblicaId, x.ElementoBiblicoId });
                    table.ForeignKey(
                        name: "FK_PerguntaElementos_ElementosBiblicos_ElementoBiblicoId",
                        column: x => x.ElementoBiblicoId,
                        principalTable: "ElementosBiblicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerguntaElementos_PerguntasBiblicas_PerguntaBiblicaId",
                        column: x => x.PerguntaBiblicaId,
                        principalTable: "PerguntasBiblicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReflexoesBiblicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerguntaBiblicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Explicacao = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    AplicacaoPratica = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PerguntaReflexao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReflexoesBiblicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReflexoesBiblicas_PerguntasBiblicas_PerguntaBiblicaId",
                        column: x => x.PerguntaBiblicaId,
                        principalTable: "PerguntasBiblicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConexaoFontesBiblicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Referencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TextoVersiculo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConexaoBiblicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConexaoFontesBiblicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConexaoFontesBiblicas_ConexoesBiblicas_ConexaoBiblicaId",
                        column: x => x.ConexaoBiblicaId,
                        principalTable: "ConexoesBiblicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ElementosBiblicos",
                columns: new[] { "Id", "Descricao", "ElementoCentral", "Nome", "Ocupacao", "PeriodoHistorico", "TipoElemento" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Filho de Deus, o Messias prometido e cumprimento de todas as profecias.", true, "Jesus", "Filho de Deus, Messias", "Século I d.C.", 1 });

            migrationBuilder.InsertData(
                table: "ElementosBiblicos",
                columns: new[] { "Id", "Descricao", "Nome", "Ocupacao", "PeriodoHistorico", "TipoElemento" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Apóstolo dos gentios, responsável por explicar a teologia de Cristo.", "Paulo", "Apóstolo, Teólogo", "Século I d.C.", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Apóstolo e líder da Igreja primitiva.", "Pedro", "Apóstolo, Pescador", "Século I d.C.", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Apóstolo e evangelista, discípulo amado de Jesus.", "João", "Apóstolo, Evangelista", "Século I d.C.", 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Profeta cujos 3 dias no ventre do peixe prefiguram a ressurreição de Cristo.", "Jonas", "Profeta", "Século VIII a.C.", 1 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Profeta e legislador, mediador da antiga aliança que prefigura Cristo.", "Moisés", "Profeta, Legislador", "Século XIII a.C.", 1 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Rei e salmista, cujo trono eterno aponta para o reinado de Cristo.", "Davi", "Rei, Salmista", "Século X a.C.", 1 }
                });

            migrationBuilder.InsertData(
                table: "ElementosBiblicos",
                columns: new[] { "Id", "Descricao", "Nome", "TipoElemento" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Confiança e crença em Deus e em Seus propósitos.", "Fé", 2 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "União entre homem e mulher que reflete a relação de Cristo e a Igreja.", "Casamento", 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "A graça de Deus que perdoa os pecados através de Cristo.", "Perdão", 2 },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "A certeza das promessas de Deus cumpridas em Cristo.", "Esperança", 2 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "A redenção do ser humano através do sacrifício de Jesus Cristo.", "Salvação", 2 }
                });

            migrationBuilder.InsertData(
                table: "PerguntasBiblicas",
                columns: new[] { "Id", "Pergunta", "Titulo" },
                values: new object[,]
                {
                    { new Guid("e1111111-e111-e111-e111-e11111111111"), "Por que Jesus citou Jonas?", "Jonas e a Ressurreição" },
                    { new Guid("e2222222-e222-e222-e222-e22222222222"), "O que a Bíblia ensina sobre casamento?", "O Que é Casamento?" }
                });

            migrationBuilder.InsertData(
                table: "ConexoesBiblicas",
                columns: new[] { "Id", "DestinoId", "Explicacao", "OrigemId", "TipoConexao" },
                values: new object[,]
                {
                    { new Guid("f1111111-f111-f111-f111-f11111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "Jonas ficou 3 dias no ventre do peixe, prefigurando os 3 dias de Jesus no sepulcro e Sua ressurreição.", new Guid("55555555-5555-5555-5555-555555555555"), 2 },
                    { new Guid("f2222222-f222-f222-f222-f22222222222"), new Guid("11111111-1111-1111-1111-111111111111"), "Moisés como mediador da antiga aliança prefigura Cristo, mediador da nova e eterna aliança.", new Guid("66666666-6666-6666-6666-666666666666"), 2 },
                    { new Guid("f3333333-f333-f333-f333-f33333333333"), new Guid("11111111-1111-1111-1111-111111111111"), "O trono eterno prometido a Davi aponta para o reino eterno de Jesus Cristo.", new Guid("77777777-7777-7777-7777-777777777777"), 1 },
                    { new Guid("f4444444-f444-f444-f444-f44444444444"), new Guid("11111111-1111-1111-1111-111111111111"), "Jesus citou Jonas explicitamente ao responder sobre o sinal do Filho do Homem (Mt 12:39-40).", new Guid("55555555-5555-5555-5555-555555555555"), 6 },
                    { new Guid("f5555555-f555-f555-f555-f55555555555"), new Guid("22222222-2222-2222-2222-222222222222"), "Paulo ensina que o casamento é reflexo da relação de Cristo com a Igreja em Efésios 5:25-32.", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 7 },
                    { new Guid("f6666666-f666-f666-f666-f66666666666"), new Guid("22222222-2222-2222-2222-222222222222"), "Paulo explica o perdão como graça de Deus em Cristo, exortando os crentes a perdoarem uns aos outros.", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 7 }
                });

            migrationBuilder.InsertData(
                table: "PerguntaElementos",
                columns: new[] { "ElementoBiblicoId", "PerguntaBiblicaId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("e1111111-e111-e111-e111-e11111111111") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("e1111111-e111-e111-e111-e11111111111") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("e2222222-e222-e222-e222-e22222222222") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("e2222222-e222-e222-e222-e22222222222") }
                });

            migrationBuilder.InsertData(
                table: "ConexaoFontesBiblicas",
                columns: new[] { "Id", "ConexaoBiblicaId", "Referencia", "TextoVersiculo" },
                values: new object[,]
                {
                    { 1, new Guid("f1111111-f111-f111-f111-f11111111111"), "Mateus 12:40", null },
                    { 2, new Guid("f2222222-f222-f222-f222-f22222222222"), "João 5:46", null },
                    { 3, new Guid("f3333333-f333-f333-f333-f33333333333"), "Lucas 20:41-44", null },
                    { 4, new Guid("f4444444-f444-f444-f444-f44444444444"), "Mateus 12:39", null },
                    { 5, new Guid("f5555555-f555-f555-f555-f55555555555"), "Efésios 5:25-32", null },
                    { 6, new Guid("f6666666-f666-f666-f666-f66666666666"), "Efésios 4:32", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConexaoFontesBiblicas_ConexaoBiblicaId",
                table: "ConexaoFontesBiblicas",
                column: "ConexaoBiblicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConexoesBiblicas_DestinoId",
                table: "ConexoesBiblicas",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConexoesBiblicas_OrigemId",
                table: "ConexoesBiblicas",
                column: "OrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaElementos_ElementoBiblicoId",
                table: "PerguntaElementos",
                column: "ElementoBiblicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReflexoesBiblicas_PerguntaBiblicaId",
                table: "ReflexoesBiblicas",
                column: "PerguntaBiblicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConexaoFontesBiblicas");

            migrationBuilder.DropTable(
                name: "PerguntaElementos");

            migrationBuilder.DropTable(
                name: "ReflexoesBiblicas");

            migrationBuilder.DropTable(
                name: "ConexoesBiblicas");

            migrationBuilder.DropTable(
                name: "PerguntasBiblicas");

            migrationBuilder.DropTable(
                name: "ElementosBiblicos");
        }
    }
}
