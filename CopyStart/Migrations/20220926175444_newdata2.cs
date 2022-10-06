using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class newdata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbfb0a7-d323-4f84-9d14-daeef363f947",
                columns: new[] { "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "CPTECNICO@GMAIL.COM", "CPTECNICO@GMAIL.COM" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cacd6063-c77d-437e-8cad-2e97ba1a0a6a",
                columns: new[] { "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "CPCLIENTE@YOPMAIL.COM", "CPCLIENTE@YOPMAIL.COM" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "f8ee70d4-3209-4003-a5b9-4b04e44875df");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "cde4de92-07fb-4269-894d-51de6888d602");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "93e65c48-ddf1-4ebf-8081-8e3a6185aec7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "fd07801a-380b-4023-9657-f0d2094a82f7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbfb0a7-d323-4f84-9d14-daeef363f947",
                columns: new[] { "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "USER3@GMAIL.COM", "USER3@GMAIL.COM" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cacd6063-c77d-437e-8cad-2e97ba1a0a6a",
                columns: new[] { "NormalizedEmail", "NormalizedUserName" },
                values: new object[] { "USER4@GMAIL.COM", "USER4@GMAIL.COM" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "4ba36195-d51b-43d0-8906-61bc590ee0a2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "09933584-2e73-4105-9125-3a8883f98a2b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "ec51f978-d90e-4003-b9eb-8c4f6fe44f40");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "563853bf-5bc4-42f1-b8b5-ed6d71fde3cc");
        }
    }
}
