using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_5.migrations
{
    /// <inheritdoc />
    public partial class Update_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicles",
                table: "vehicles");

            migrationBuilder.RenameTable(
                name: "vehicles",
                newName: "Vehicles");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "Sells",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "Repairs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "VIN");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_VIN",
                table: "Sells",
                column: "VIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_VIN",
                table: "Repairs",
                column: "VIN");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_VIN",
                table: "Purchases",
                column: "VIN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Vehicles_VIN",
                table: "Purchases",
                column: "VIN",
                principalTable: "Vehicles",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Vehicles_VIN",
                table: "Repairs",
                column: "VIN",
                principalTable: "Vehicles",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Vehicles_VIN",
                table: "Sells",
                column: "VIN",
                principalTable: "Vehicles",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Vehicles_VIN",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Vehicles_VIN",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Vehicles_VIN",
                table: "Sells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Sells_VIN",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_VIN",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_VIN",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VIN",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "VIN",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "VIN",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "vehicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicles",
                table: "vehicles",
                column: "VIN");
        }
    }
}
