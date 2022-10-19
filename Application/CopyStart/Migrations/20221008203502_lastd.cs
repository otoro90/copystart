using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class lastd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "TEC", "3cbfb0a7-d323-4f84-9d14-daeef363f947" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "COORD", "5c141fdd-ab2f-49be-8a31-43aec63f918f" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "CLN", "cacd6063-c77d-437e-8cad-2e97ba1a0a6a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbfb0a7-d323-4f84-9d14-daeef363f947");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5c141fdd-ab2f-49be-8a31-43aec63f918f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cacd6063-c77d-437e-8cad-2e97ba1a0a6a");

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"));

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"));

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"));

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "Personas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "Personas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Apellidos", "Direccion", "Estado", "Nombres", "NumeroDocumento", "Telefono", "TipoDocumentoId", "UbicacionId" },
                values: new object[,]
                {
                    { new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"), "Por defecto", "Calle 40, #33-18", "Disponible", "Tecnico", "1000000002", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "50001" },
                    { new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"), "Por defecto", "Calle 40, #33-18", "Activo", "Cliente", "1000000003", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "50001" },
                    { new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"), "Por defecto", "Calle 40, #33-18", "Activo", "Coordinador", "1000000001", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "50001" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "4c7e0d63-0272-447e-b865-9ef967c41b54");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "9979073e-7cd1-4513-bab9-b5907894d20d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "2e278149-8f4a-4a3a-b12e-459975d69bc9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "0506b14e-a636-4c00-98d0-b22850c0f81b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonaId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3cbfb0a7-d323-4f84-9d14-daeef363f947", 0, "aa35d6da-54e6-476b-8810-4a182b8b07de", "cptecnico@yopmail.com", true, true, null, "CPTECNICO@YOPMAIL.COM", "CPTECNICO@YOPMAIL.COM", "AQAAAAEAACcQAAAAEHRn5OJw1NKAwjB4w6dEsVjB2qi/bOR+8/WbmTjTa8lQCvTy6Xg4WqMtm6A/yYYxDw==", new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"), null, false, "4LNTJIEMTON6KDXWASKMDXTFM2BCUCH6", false, "cptecnico@yopmail.com" },
                    { "5c141fdd-ab2f-49be-8a31-43aec63f918f", 0, "776e1d1a-0c3c-41b0-bb44-bcb151add354", "cpcoordinador@yopmail.com", true, true, null, "CPCOORDINADOR@YOPMAIL.COM", "CPCOORDINADOR@YOPMAIL.COM", "AQAAAAEAACcQAAAAEDv98UwloGC1tI3Elt0TgyWk9DxWPi475t9/P2SOAO/TgHilR1/Tnp0kQpnctazOJw==", new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"), null, false, "RZABU4JPNDQVJF4LWR5ZS755BS7H6HDR", false, "cpcoordinador@yopmail.com" },
                    { "cacd6063-c77d-437e-8cad-2e97ba1a0a6a", 0, "e3330e49-ac28-4952-a50a-a30e8222aed7", "cpcliente@yopmail.com", true, true, null, "CPCLIENTE@YOPMAIL.COM", "CPCLIENTE@YOPMAIL.COM", "AQAAAAEAACcQAAAAEGAsB2jMI6Q3cJTunTexm1lX3qqiVzHMV9sDws+mh3JcqAGY2og313y5uNjZ+32OAg==", new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"), null, false, "WJR7XMGC332UT234BG7O35M6MIIGKQ2Q", false, "cpcliente@yopmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "TEC", "3cbfb0a7-d323-4f84-9d14-daeef363f947" },
                    { "COORD", "5c141fdd-ab2f-49be-8a31-43aec63f918f" },
                    { "CLN", "cacd6063-c77d-437e-8cad-2e97ba1a0a6a" }
                });
        }
    }
}
