using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class lols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas");

            migrationBuilder.AlterColumn<string>(
                name: "UbicacionId",
                table: "Personas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "65360229-128c-440a-8e11-f10292071605");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "8690d3af-358c-4018-9add-996257fb63e5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "5e3ea73e-397c-405b-aea3-e38f18ea42bf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "63be5bbf-55f4-47c6-8227-bba33bf112e2");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoMunicipio",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas");

            migrationBuilder.AlterColumn<string>(
                name: "UbicacionId",
                table: "Personas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "9350f7cb-7699-4782-abb5-810452675349");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "9600ffd8-d2aa-4311-9310-11cfb7452a6e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "ed144eda-1055-437a-ac0c-b5fc08f4d3e5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "486d4501-794a-49f8-b99d-9b14b2c148e5");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoMunicipio");
        }
    }
}
