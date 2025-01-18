using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityTransactionWithType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44cd3643-771a-4606-84dd-3558d6e0d33f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52115d1d-925d-4dd9-a38f-e4ca238be97e");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43361d1d-21fd-4084-ab4c-6302c394a342", null, "Admin", "ADMIN" },
                    { "85ae7732-7085-4dcb-be63-4cb57bcbd5b2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43361d1d-21fd-4084-ab4c-6302c394a342");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85ae7732-7085-4dcb-be63-4cb57bcbd5b2");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "type");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44cd3643-771a-4606-84dd-3558d6e0d33f", null, "Admin", "ADMIN" },
                    { "52115d1d-925d-4dd9-a38f-e4ca238be97e", null, "User", "USER" }
                });
        }
    }
}
