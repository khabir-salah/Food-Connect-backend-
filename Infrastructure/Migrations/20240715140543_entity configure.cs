using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityconfigure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rating_RecipentId",
                table: "Rating",
                column: "RecipentId",
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
                name: "IX_Donation_DonorId",
                table: "Donation",
                column: "DonorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PickUp_Donation_DonationId",
                table: "PickUp",
                column: "DonationId",
                principalTable: "Donation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PickUp_Recipent_RecipentId",
                table: "PickUp",
                column: "RecipentId",
                principalTable: "Recipent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Recipent_RecipentId",
                table: "Rating",
                column: "RecipentId",
                principalTable: "Recipent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation");

            migrationBuilder.DropForeignKey(
                name: "FK_PickUp_Donation_DonationId",
                table: "PickUp");

            migrationBuilder.DropForeignKey(
                name: "FK_PickUp_Recipent_RecipentId",
                table: "PickUp");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Recipent_RecipentId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RecipentId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_PickUp_DonationId",
                table: "PickUp");

            migrationBuilder.DropIndex(
                name: "IX_PickUp_RecipentId",
                table: "PickUp");

            migrationBuilder.DropIndex(
                name: "IX_Donation_DonorId",
                table: "Donation");
        }
    }
}
