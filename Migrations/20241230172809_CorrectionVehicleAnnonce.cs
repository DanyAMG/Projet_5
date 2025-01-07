using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionVehicleAnnonce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_VehiculeAnnonce_VehiculeAnnonceId",
                table: "Announcements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c039d5-602d-4f59-9aad-734b2ca6e91d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc30d20f-38a0-49eb-9624-c38a5eac032a");

            migrationBuilder.RenameColumn(
                name: "VehiculeAnnonceId",
                table: "Announcements",
                newName: "VehicleAnnonceId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_VehiculeAnnonceId",
                table: "Announcements",
                newName: "IX_Announcements_VehicleAnnonceId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_VehiculeAnnonce_VehicleAnnonceId",
                table: "Announcements",
                column: "VehicleAnnonceId",
                principalTable: "VehiculeAnnonce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_VehiculeAnnonce_VehicleAnnonceId",
                table: "Announcements");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ebd25ce-08d6-4e6b-8944-74eb0b3933f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d36f66a1-5aac-46fd-804c-3f8859c5193b");

            migrationBuilder.RenameColumn(
                name: "VehicleAnnonceId",
                table: "Announcements",
                newName: "VehiculeAnnonceId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_VehicleAnnonceId",
                table: "Announcements",
                newName: "IX_Announcements_VehiculeAnnonceId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c039d5-602d-4f59-9aad-734b2ca6e91d", null, "Admin", "ADMIN" },
                    { "cc30d20f-38a0-49eb-9624-c38a5eac032a", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_VehiculeAnnonce_VehiculeAnnonceId",
                table: "Announcements",
                column: "VehiculeAnnonceId",
                principalTable: "VehiculeAnnonce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
