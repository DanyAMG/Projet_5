using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c039d5-602d-4f59-9aad-734b2ca6e91d", null, "Employee", "EMPLOYEE" },
                    { "cc30d20f-38a0-49eb-9624-c38a5eac032a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c039d5-602d-4f59-9aad-734b2ca6e91d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc30d20f-38a0-49eb-9624-c38a5eac032a");
        }
    }
}
