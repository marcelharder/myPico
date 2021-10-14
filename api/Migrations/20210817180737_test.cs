using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PicoUnits_picoUnitId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_picoUnitId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PicoUnit",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "picoUnitId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "puUnitId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_puUnitId",
                table: "Appointments",
                column: "puUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PicoUnits_puUnitId",
                table: "Appointments",
                column: "puUnitId",
                principalTable: "PicoUnits",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PicoUnits_puUnitId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_puUnitId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "puUnitId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "PicoUnit",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "picoUnitId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_picoUnitId",
                table: "Appointments",
                column: "picoUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PicoUnits_picoUnitId",
                table: "Appointments",
                column: "picoUnitId",
                principalTable: "PicoUnits",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
