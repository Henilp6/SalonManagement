using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSalonbranchxToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalonBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearofEstablishment = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonBranch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerMobileNumber = table.Column<int>(type: "int", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timming = table.Column<int>(type: "int", nullable: false),
                    SalonBranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_SalonBranch_SalonBranchId",
                        column: x => x.SalonBranchId,
                        principalTable: "SalonBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalonBranchXGenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonBranchId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonBranchXGenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalonBranchXGenders_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalonBranchXGenders_SalonBranch_SalonBranchId",
                        column: x => x.SalonBranchId,
                        principalTable: "SalonBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalonBranchXPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonBranchId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonBranchXPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalonBranchXPayments_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalonBranchXPayments_SalonBranch_SalonBranchId",
                        column: x => x.SalonBranchId,
                        principalTable: "SalonBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalonBranchXServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonBranchId = table.Column<int>(type: "int", nullable: false),
                    FirstServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonBranchXServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalonBranchXServices_FirstServices_FirstServiceId",
                        column: x => x.FirstServiceId,
                        principalTable: "FirstServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalonBranchXServices_SalonBranch_SalonBranchId",
                        column: x => x.SalonBranchId,
                        principalTable: "SalonBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SalonBranchId",
                table: "Bookings",
                column: "SalonBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXGenders_GenderId",
                table: "SalonBranchXGenders",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXGenders_SalonBranchId",
                table: "SalonBranchXGenders",
                column: "SalonBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXPayments_PaymentId",
                table: "SalonBranchXPayments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXPayments_SalonBranchId",
                table: "SalonBranchXPayments",
                column: "SalonBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXServices_FirstServiceId",
                table: "SalonBranchXServices",
                column: "FirstServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchXServices_SalonBranchId",
                table: "SalonBranchXServices",
                column: "SalonBranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "SalonBranchXGenders");

            migrationBuilder.DropTable(
                name: "SalonBranchXPayments");

            migrationBuilder.DropTable(
                name: "SalonBranchXServices");

            migrationBuilder.DropTable(
                name: "SalonBranch");
        }
    }
}
