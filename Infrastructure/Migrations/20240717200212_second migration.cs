using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation");

            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropTable(
                name: "PickUp");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Donation_DonorId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DonorId",
                table: "Donation",
                newName: "RecipentId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Donation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId",
                table: "Donation",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OragizationId",
                table: "Donation",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "Donation",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "PickUpLocation",
                table: "Donation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpTime",
                table: "Donation",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProfileImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FamilyCount = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOcalGovernment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Family_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProfileImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOcalGovernment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manager_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OganisationName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProfileImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CacNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOcalGovernment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FamilyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OragizationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RecipentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Review = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReviewImages = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrganisationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCollection_Donation_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodCollection_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodCollection_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodCollection_Recipent_RecipentId",
                        column: x => x.RecipentId,
                        principalTable: "Recipent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_FamilyId",
                table: "Donation",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_OrganisationId",
                table: "Donation",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_RecipentId",
                table: "Donation",
                column: "RecipentId");

            migrationBuilder.CreateIndex(
                name: "IX_Family_UserId",
                table: "Family",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodCollection_DonationId",
                table: "FoodCollection",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCollection_FamilyId",
                table: "FoodCollection",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCollection_OrganisationId",
                table: "FoodCollection",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCollection_RecipentId",
                table: "FoodCollection",
                column: "RecipentId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserId",
                table: "Manager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_UserId",
                table: "Organisation",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Family_FamilyId",
                table: "Donation",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Organisation_OrganisationId",
                table: "Donation",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Recipent_RecipentId",
                table: "Donation",
                column: "RecipentId",
                principalTable: "Recipent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Family_FamilyId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Organisation_OrganisationId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Recipent_RecipentId",
                table: "Donation");

            migrationBuilder.DropTable(
                name: "FoodCollection");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Donation_FamilyId",
                table: "Donation");

            migrationBuilder.DropIndex(
                name: "IX_Donation_OrganisationId",
                table: "Donation");

            migrationBuilder.DropIndex(
                name: "IX_Donation_RecipentId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "OragizationId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PickUpLocation",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PickUpTime",
                table: "Donation");

            migrationBuilder.RenameColumn(
                name: "RecipentId",
                table: "Donation",
                newName: "DonorId");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "Donation",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOcalGovernment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfileImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PickUp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PickUpLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PickUpStatus = table.Column<int>(type: "int", nullable: false),
                    PickUpTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecipentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickUp_Donation_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickUp_Recipent_RecipentId",
                        column: x => x.RecipentId,
                        principalTable: "Recipent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DonationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RecipentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Review = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReviewImages = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Recipent_RecipentId",
                        column: x => x.RecipentId,
                        principalTable: "Recipent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donation_DonorId",
                table: "Donation",
                column: "DonorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donor_UserId",
                table: "Donor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickUp_DonationId",
                table: "PickUp",
                column: "DonationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickUp_RecipentId",
                table: "PickUp",
                column: "RecipentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RecipentId",
                table: "Rating",
                column: "RecipentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
