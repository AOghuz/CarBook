using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentACarProcesses");

            migrationBuilder.DropTable(
                name: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerSurname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "RentACarProcesses",
                columns: table => new
                {
                    RentACarProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    DropOffDate = table.Column<DateTime>(type: "Date", nullable: false),
                    DropOffDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropOffLocation = table.Column<int>(type: "int", nullable: false),
                    DropOffTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PickUpDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpLocation = table.Column<int>(type: "int", nullable: false),
                    PickUpTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RentACarProcessID1 = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentACarProcesses", x => x.RentACarProcessID);
                    table.ForeignKey(
                        name: "FK_RentACarProcesses_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentACarProcesses_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentACarProcesses_RentACarProcesses_RentACarProcessID1",
                        column: x => x.RentACarProcessID1,
                        principalTable: "RentACarProcesses",
                        principalColumn: "RentACarProcessID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcesses_CarID",
                table: "RentACarProcesses",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcesses_CustomerID",
                table: "RentACarProcesses",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcesses_RentACarProcessID1",
                table: "RentACarProcesses",
                column: "RentACarProcessID1");
        }
    }
}
