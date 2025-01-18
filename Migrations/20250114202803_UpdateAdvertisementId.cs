using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdvertisementId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Advertisements_AdvertisementId",
                table: "Repairs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "128b0ba0-f5b8-4f96-8afe-7890a00661d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190cb3b6-7aa9-428e-83d2-14a6aa7ab769");

            migrationBuilder.RenameColumn(
                name: "AdvertisementId",
                table: "Repairs",
                newName: "AdvertisementsId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_AdvertisementId",
                table: "Repairs",
                newName: "IX_Repairs_AdvertisementsId");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b01ed31-09df-4ed5-b382-9ac5e684e998", null, "User", "USER" },
                    { "45abef28-cec0-4205-b6c1-4bfc88e037da", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Advertisements_AdvertisementsId",
                table: "Repairs",
                column: "AdvertisementsId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Advertisements_AdvertisementsId",
                table: "Repairs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b01ed31-09df-4ed5-b382-9ac5e684e998");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45abef28-cec0-4205-b6c1-4bfc88e037da");

            migrationBuilder.RenameColumn(
                name: "AdvertisementsId",
                table: "Repairs",
                newName: "AdvertisementId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_AdvertisementsId",
                table: "Repairs",
                newName: "IX_Repairs_AdvertisementId");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "128b0ba0-f5b8-4f96-8afe-7890a00661d6", null, "User", "USER" },
                    { "190cb3b6-7aa9-428e-83d2-14a6aa7ab769", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Advertisements_AdvertisementId",
                table: "Repairs",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
