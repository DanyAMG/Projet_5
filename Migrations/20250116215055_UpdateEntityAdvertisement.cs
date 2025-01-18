using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityAdvertisement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b01ed31-09df-4ed5-b382-9ac5e684e998");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45abef28-cec0-4205-b6c1-4bfc88e037da");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "BuyingPrice");

            migrationBuilder.AddColumn<bool>(
                name: "SellingPrice",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "197f789d-1fbb-45f8-9d7f-947af20b2eb4", null, "User", "USER" },
                    { "cf9213cc-70fd-423d-b0ae-ad45060c0a57", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "197f789d-1fbb-45f8-9d7f-947af20b2eb4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf9213cc-70fd-423d-b0ae-ad45060c0a57");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "BuyingPrice",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b01ed31-09df-4ed5-b382-9ac5e684e998", null, "User", "USER" },
                    { "45abef28-cec0-4205-b6c1-4bfc88e037da", null, "Admin", "ADMIN" }
                });
        }
    }
}
