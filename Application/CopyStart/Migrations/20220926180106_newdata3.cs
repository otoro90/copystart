using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class newdata3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe",
                column: "Email",
                value: "cpadmin@yopmail.com");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbfb0a7-d323-4f84-9d14-daeef363f947",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "cptecnico@yopmail.com", "CPTECNICO@YOPMAIL.COM", "CPTECNICO@YOPMAIL.COM", "cptecnico@yopmail.com" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe",
                column: "Email",
                value: "cpadmin@gmail.com");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbfb0a7-d323-4f84-9d14-daeef363f947",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "cptecnico@gmail.com", "CPTECNICO@GMAIL.COM", "CPTECNICO@GMAIL.COM", "cptecnico@gmail.com" });

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
    }
}
