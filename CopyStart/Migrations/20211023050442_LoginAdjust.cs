using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class LoginAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Users_UserId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_UserId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Personas");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonaId",
                table: "Users",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Personas_PersonaId",
                table: "Users",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Personas_PersonaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserTokens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserLogins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserClaims",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RoleClaims",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Personas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UserId",
                table: "Personas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Users_UserId",
                table: "Personas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
