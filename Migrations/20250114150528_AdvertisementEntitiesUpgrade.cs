using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class AdvertisementEntitiesUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41bc1d3f-4f47-4470-8256-456d273fd9a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66bfef11-3625-4de1-a3a5-e7b5be5f5223");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "128b0ba0-f5b8-4f96-8afe-7890a00661d6", null, "User", "USER" },
                    { "190cb3b6-7aa9-428e-83d2-14a6aa7ab769", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "128b0ba0-f5b8-4f96-8afe-7890a00661d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190cb3b6-7aa9-428e-83d2-14a6aa7ab769");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41bc1d3f-4f47-4470-8256-456d273fd9a5", null, "Admin", "ADMIN" },
                    { "66bfef11-3625-4de1-a3a5-e7b5be5f5223", null, "User", "USER" }
                });
        }
    }
}
