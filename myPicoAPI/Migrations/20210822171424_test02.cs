using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class test02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateNumbers_Months_mmMonthId",
                table: "DateNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOccupancy_Months_mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateOccupancy_mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateNumbers_mmMonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "mmMonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "RequestedDays",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelMonthId",
                table: "DateOccupancy",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "DateOccupancy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "picoUnit",
                table: "DateOccupancy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelMonthId",
                table: "DateNumbers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "DateNumbers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DateOccupancy_Month_ModelMonthId",
                table: "DateOccupancy",
                column: "Month_ModelMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_DateNumbers_Month_ModelMonthId",
                table: "DateNumbers",
                column: "Month_ModelMonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_DateNumbers_Months_Month_ModelMonthId",
                table: "DateNumbers",
                column: "Month_ModelMonthId",
                principalTable: "Months",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateOccupancy_Months_Month_ModelMonthId",
                table: "DateOccupancy",
                column: "Month_ModelMonthId",
                principalTable: "Months",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateNumbers_Months_Month_ModelMonthId",
                table: "DateNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOccupancy_Months_Month_ModelMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateOccupancy_Month_ModelMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateNumbers_Month_ModelMonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "Month_ModelMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "picoUnit",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "Month_ModelMonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "DateNumbers");

            migrationBuilder.AddColumn<int>(
                name: "mmMonthId",
                table: "DateOccupancy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mmMonthId",
                table: "DateNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedDays",
                table: "Appointments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DateOccupancy_mmMonthId",
                table: "DateOccupancy",
                column: "mmMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_DateNumbers_mmMonthId",
                table: "DateNumbers",
                column: "mmMonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_DateNumbers_Months_mmMonthId",
                table: "DateNumbers",
                column: "mmMonthId",
                principalTable: "Months",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DateOccupancy_Months_mmMonthId",
                table: "DateOccupancy",
                column: "mmMonthId",
                principalTable: "Months",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
