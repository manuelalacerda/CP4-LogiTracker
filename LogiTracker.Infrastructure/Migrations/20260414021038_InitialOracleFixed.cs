using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialOracleFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id        = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name      = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: false),
                    TaxId     = table.Column<string>(type: "VARCHAR2(18)", maxLength: 18, nullable: false),
                    Active    = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_TaxId",
                table: "Carriers",
                column: "TaxId",
                unique: true);

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id            = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Description   = table.Column<string>(type: "VARCHAR2(500)", maxLength: 500, nullable: false),
                    Weight        = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MonetaryValue = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Active        = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt     = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id        = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Plate     = table.Column<string>(type: "VARCHAR2(10)", maxLength: 10, nullable: false),
                    Model     = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    CarrierId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Active    = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarrierId",
                table: "Vehicles",
                column: "CarrierId");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id            = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name          = table.Column<string>(type: "VARCHAR2(200)", maxLength: 200, nullable: false),
                    LicenseNumber = table.Column<string>(type: "VARCHAR2(20)", maxLength: 20, nullable: false),
                    CarrierId     = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Active        = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt     = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenseNumber",
                table: "Drivers",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CarrierId",
                table: "Drivers",
                column: "CarrierId");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id        = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Status    = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    OrderDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VehicleId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DriverId  = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CargoId   = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Active    = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CargoId",
                table: "Deliveries",
                column: "CargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DriverId",
                table: "Deliveries",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_VehicleId",
                table: "Deliveries",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Deliveries");
            migrationBuilder.DropTable(name: "Drivers");
            migrationBuilder.DropTable(name: "Vehicles");
            migrationBuilder.DropTable(name: "Cargos");
            migrationBuilder.DropTable(name: "Carriers");
        }
    }
}
