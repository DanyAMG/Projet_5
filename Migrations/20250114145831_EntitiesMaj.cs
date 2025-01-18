using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesMaj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "300a14cb-9bd8-4f16-8e66-8dc14fb8b9e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be57049-a8fc-416a-af8f-f7b0fd8080ff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41bc1d3f-4f47-4470-8256-456d273fd9a5", null, "Admin", "ADMIN" },
                    { "66bfef11-3625-4de1-a3a5-e7b5be5f5223", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "300a14cb-9bd8-4f16-8e66-8dc14fb8b9e8", null, "Admin", "ADMIN" },
                    { "8be57049-a8fc-416a-af8f-f7b0fd8080ff", null, "User", "USER" }
                });
        }
    }
}
