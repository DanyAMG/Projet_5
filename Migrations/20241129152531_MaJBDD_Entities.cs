using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_5.Migrations
{
    /// <inheritdoc />
    public partial class MaJBDD_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId1",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Sells");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleId1",
                table: "Repairs",
                newName: "VehiculeAnnonceId");

            migrationBuilder.RenameColumn(
                name: "ReparationCost",
                table: "Repairs",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "RepaireType",
                table: "Repairs",
                newName: "Reparation");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_VehicleId1",
                table: "Repairs",
                newName: "IX_Repairs_VehiculeAnnonceId");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Vehicles",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Finition",
                table: "Vehicles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId",
                table: "Repairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehiculeAnnonce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculeAnnonce", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiculeAnnonce_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisponibilityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponibility = table.Column<bool>(type: "bit", nullable: false),
                    selled = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehiculeAnnonceId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcements_VehiculeAnnonce_VehiculeAnnonceId",
                        column: x => x.VehiculeAnnonceId,
                        principalTable: "VehiculeAnnonce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    AnnouncementId = table.Column<int>(type: "int", nullable: true),
                    VehiculeAnnonceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_VehiculeAnnonce_VehiculeAnnonceId",
                        column: x => x.VehiculeAnnonceId,
                        principalTable: "VehiculeAnnonce",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_AnnouncementId",
                table: "Repairs",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_VehicleId",
                table: "Announcements",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_VehiculeAnnonceId",
                table: "Announcements",
                column: "VehiculeAnnonceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AnnouncementId",
                table: "Transactions",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VehicleId",
                table: "Transactions",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VehiculeAnnonceId",
                table: "Transactions",
                column: "VehiculeAnnonceId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculeAnnonce_VehicleId",
                table: "VehiculeAnnonce",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Announcements_AnnouncementId",
                table: "Repairs",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId",
                table: "Repairs",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_VehiculeAnnonce_VehiculeAnnonceId",
                table: "Repairs",
                column: "VehiculeAnnonceId",
                principalTable: "VehiculeAnnonce",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Announcements_AnnouncementId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_VehiculeAnnonce_VehiculeAnnonceId",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "VehiculeAnnonce");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_AnnouncementId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "AnnouncementId",
                table: "Repairs");

            migrationBuilder.RenameColumn(
                name: "VehiculeAnnonceId",
                table: "Repairs",
                newName: "VehicleId1");

            migrationBuilder.RenameColumn(
                name: "Reparation",
                table: "Repairs",
                newName: "RepaireType");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Repairs",
                newName: "ReparationCost");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_VehiculeAnnonceId",
                table: "Repairs",
                newName: "IX_Repairs_VehicleId1");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Finition",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    BuyingPrice = table.Column<float>(type: "real", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    VehicleId1 = table.Column<int>(type: "int", nullable: true),
                    VehicleId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Vehicles_VehicleId1",
                        column: x => x.VehicleId1,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_Vehicles_VehicleId2",
                        column: x => x.VehicleId2,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    SellingDate = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    VehicleId1 = table.Column<int>(type: "int", nullable: true),
                    VehicleId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sells_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sells_Vehicles_VehicleId1",
                        column: x => x.VehicleId1,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sells_Vehicles_VehicleId2",
                        column: x => x.VehicleId2,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_VehicleId",
                table: "Purchases",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_VehicleId1",
                table: "Purchases",
                column: "VehicleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_VehicleId2",
                table: "Purchases",
                column: "VehicleId2",
                unique: true,
                filter: "[VehicleId2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_VehicleId",
                table: "Sells",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_VehicleId1",
                table: "Sells",
                column: "VehicleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_VehicleId2",
                table: "Sells",
                column: "VehicleId2",
                unique: true,
                filter: "[VehicleId2] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId",
                table: "Repairs",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Vehicles_VehicleId1",
                table: "Repairs",
                column: "VehicleId1",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
