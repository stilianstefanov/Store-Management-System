using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditService.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Purchases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Borrowers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Borrowers");
        }
    }
}
