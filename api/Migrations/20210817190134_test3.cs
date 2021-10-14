using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateNumbers_Months_Month_ModelId",
                table: "DateNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOccupancy_Months_Month_ModelId",
                table: "DateOccupancy");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Months",
                table: "Months");

            migrationBuilder.DropIndex(
                name: "IX_DateOccupancy_Month_ModelId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateNumbers_Month_ModelId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Months");

            migrationBuilder.DropColumn(
                name: "Month_ModelId",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "Month_ModelId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "picoUnitPhotoUrl",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "Months",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "DateOccupancy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "mmMonthId",
                table: "DateOccupancy",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "DateNumbers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "mmMonthId",
                table: "DateNumbers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Months",
                table: "Months",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateNumbers_Months_mmMonthId",
                table: "DateNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOccupancy_Months_mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Months",
                table: "Months");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_DateOccupancy_mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropIndex(
                name: "IX_DateNumbers_mmMonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Months");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "mmMonthId",
                table: "DateOccupancy");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "DateNumbers");

            migrationBuilder.DropColumn(
                name: "mmMonthId",
                table: "DateNumbers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Months",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelId",
                table: "DateOccupancy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month_ModelId",
                table: "DateNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "picoUnitPhotoUrl",
                table: "Appointments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Months",
                table: "Months",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DateOccupancy_Month_ModelId",
                table: "DateOccupancy",
                column: "Month_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DateNumbers_Month_ModelId",
                table: "DateNumbers",
                column: "Month_ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DateNumbers_Months_Month_ModelId",
                table: "DateNumbers",
                column: "Month_ModelId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateOccupancy_Months_Month_ModelId",
                table: "DateOccupancy",
                column: "Month_ModelId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
