using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityconfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "User",
                newName: "ConfirmPassword");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Recipent",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipentId",
                table: "Rating",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Donor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipent_UserId",
                table: "Recipent",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donor_UserId",
                table: "Donor",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donor_User_UserId",
                table: "Donor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipent_User_UserId",
                table: "Recipent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donor_User_UserId",
                table: "Donor");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipent_User_UserId",
                table: "Recipent");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Recipent_UserId",
                table: "Recipent");

            migrationBuilder.DropIndex(
                name: "IX_Donor_UserId",
                table: "Donor");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Recipent");

            migrationBuilder.DropColumn(
                name: "RecipentId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Donor");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "User",
                newName: "ProfileImage");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                table: "User",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Role",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId1",
                table: "User",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId1",
                table: "User",
                column: "RoleId1",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
