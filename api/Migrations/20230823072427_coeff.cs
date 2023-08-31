using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class coeff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USDEUR",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "USDJPY",
                table: "Currency");

            migrationBuilder.AddColumn<double>(
                name: "EURPHP",
                table: "Currency",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "JPYPHP",
                table: "Currency",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EURPHP",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "JPYPHP",
                table: "Currency");

            migrationBuilder.AddColumn<double>(
                name: "USDEUR",
                table: "Currency",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "USDJPY",
                table: "Currency",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
