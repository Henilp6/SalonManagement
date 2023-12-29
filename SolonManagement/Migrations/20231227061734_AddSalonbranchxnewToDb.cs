using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSalonbranchxnewToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_SalonBranch_SalonBranchId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXGenders_SalonBranch_SalonBranchId",
                table: "SalonBranchXGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXPayments_SalonBranch_SalonBranchId",
                table: "SalonBranchXPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXServices_SalonBranch_SalonBranchId",
                table: "SalonBranchXServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalonBranch",
                table: "SalonBranch");

            migrationBuilder.DropColumn(
                name: "City",
                table: "SalonBranch");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "SalonBranch");

            migrationBuilder.DropColumn(
                name: "State",
                table: "SalonBranch");

            migrationBuilder.RenameTable(
                name: "SalonBranch",
                newName: "SalonBranchs");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "SalonBranchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "SalonBranchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "SalonBranchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalonBranchs",
                table: "SalonBranchs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchs_CityId",
                table: "SalonBranchs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchs_CountryId",
                table: "SalonBranchs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalonBranchs_StateId",
                table: "SalonBranchs",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_SalonBranchs_SalonBranchId",
                table: "Bookings",
                column: "SalonBranchId",
                principalTable: "SalonBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchs_Cities_CityId",
                table: "SalonBranchs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchs_Countries_CountryId",
                table: "SalonBranchs",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchs_States_StateId",
                table: "SalonBranchs",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXGenders_SalonBranchs_SalonBranchId",
                table: "SalonBranchXGenders",
                column: "SalonBranchId",
                principalTable: "SalonBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXPayments_SalonBranchs_SalonBranchId",
                table: "SalonBranchXPayments",
                column: "SalonBranchId",
                principalTable: "SalonBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXServices_SalonBranchs_SalonBranchId",
                table: "SalonBranchXServices",
                column: "SalonBranchId",
                principalTable: "SalonBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_SalonBranchs_SalonBranchId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchs_Cities_CityId",
                table: "SalonBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchs_Countries_CountryId",
                table: "SalonBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchs_States_StateId",
                table: "SalonBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXGenders_SalonBranchs_SalonBranchId",
                table: "SalonBranchXGenders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXPayments_SalonBranchs_SalonBranchId",
                table: "SalonBranchXPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SalonBranchXServices_SalonBranchs_SalonBranchId",
                table: "SalonBranchXServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalonBranchs",
                table: "SalonBranchs");

            migrationBuilder.DropIndex(
                name: "IX_SalonBranchs_CityId",
                table: "SalonBranchs");

            migrationBuilder.DropIndex(
                name: "IX_SalonBranchs_CountryId",
                table: "SalonBranchs");

            migrationBuilder.DropIndex(
                name: "IX_SalonBranchs_StateId",
                table: "SalonBranchs");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "SalonBranchs");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "SalonBranchs");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "SalonBranchs");

            migrationBuilder.RenameTable(
                name: "SalonBranchs",
                newName: "SalonBranch");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "SalonBranch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "SalonBranch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "SalonBranch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalonBranch",
                table: "SalonBranch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_SalonBranch_SalonBranchId",
                table: "Bookings",
                column: "SalonBranchId",
                principalTable: "SalonBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXGenders_SalonBranch_SalonBranchId",
                table: "SalonBranchXGenders",
                column: "SalonBranchId",
                principalTable: "SalonBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXPayments_SalonBranch_SalonBranchId",
                table: "SalonBranchXPayments",
                column: "SalonBranchId",
                principalTable: "SalonBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonBranchXServices_SalonBranch_SalonBranchId",
                table: "SalonBranchXServices",
                column: "SalonBranchId",
                principalTable: "SalonBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
