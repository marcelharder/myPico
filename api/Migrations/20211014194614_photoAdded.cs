using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class photoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateNumbers_Months_Month_ModelMonthId",
                table: "DateNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOccupancy_Months_Month_ModelMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropTable(
                name: "Months");

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
                name: "Month_ModelMonthId",
                table: "DateNumbers");

            migrationBuilder.AddColumn<string>(
                name: "Main_Photo_Url",
                table: "PicoUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Main_Photo_Url",
                table: "PicoUnits");

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelMonthId",
                table: "DateOccupancy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelMonthId",
                table: "DateNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    MonthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PicoUnit = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.MonthId);
                });

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
    }
}
