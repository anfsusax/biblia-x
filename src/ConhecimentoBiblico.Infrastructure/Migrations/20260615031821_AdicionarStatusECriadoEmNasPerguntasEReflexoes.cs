using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConhecimentoBiblico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarStatusECriadoEmNasPerguntasEReflexoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PerguntaReflexao",
                table: "ReflexoesBiblicas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Explicacao",
                table: "ReflexoesBiblicas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "AplicacaoPratica",
                table: "ReflexoesBiblicas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "ReflexoesBiblicas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "PerguntasBiblicas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PerguntasBiblicas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PerguntasBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("e1111111-e111-e111-e111-e11111111111"),
                columns: new[] { "CriadoEm", "Status" },
                values: new object[] { new DateTime(2026, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.UpdateData(
                table: "PerguntasBiblicas",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-e222-e222-e222-e22222222222"),
                columns: new[] { "CriadoEm", "Status" },
                values: new object[] { new DateTime(2026, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "ReflexoesBiblicas");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "PerguntasBiblicas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PerguntasBiblicas");

            migrationBuilder.AlterColumn<string>(
                name: "PerguntaReflexao",
                table: "ReflexoesBiblicas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Explicacao",
                table: "ReflexoesBiblicas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AplicacaoPratica",
                table: "ReflexoesBiblicas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);
        }
    }
}
