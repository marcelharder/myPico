using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PicoUnit = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PicoUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ownerId = table.Column<int>(nullable: false),
                    picoUnitNumber = table.Column<string>(nullable: true),
                    LowSeasonRent = table.Column<float>(nullable: false),
                    MidSeasonRent = table.Column<float>(nullable: false),
                    HighSeasonRent = table.Column<float>(nullable: false),
                    DiscountPercentage = table.Column<float>(nullable: false),
                    Iban = table.Column<string>(nullable: true),
                    BankAddress = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    Swift = table.Column<string>(nullable: true),
                    Caretaker = table.Column<string>(nullable: true),
                    CaretakerMobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicoUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    KnownAs = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    LookingFor = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Interests = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    DatabaseRole = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Month_ModelId = table.Column<int>(nullable: false),
                    day_1 = table.Column<int>(nullable: false),
                    day_2 = table.Column<int>(nullable: false),
                    day_3 = table.Column<int>(nullable: false),
                    day_4 = table.Column<int>(nullable: false),
                    day_5 = table.Column<int>(nullable: false),
                    day_6 = table.Column<int>(nullable: false),
                    day_7 = table.Column<int>(nullable: false),
                    day_8 = table.Column<int>(nullable: false),
                    day_9 = table.Column<int>(nullable: false),
                    day_10 = table.Column<int>(nullable: false),
                    day_11 = table.Column<int>(nullable: false),
                    day_12 = table.Column<int>(nullable: false),
                    day_13 = table.Column<int>(nullable: false),
                    day_14 = table.Column<int>(nullable: false),
                    day_15 = table.Column<int>(nullable: false),
                    day_16 = table.Column<int>(nullable: false),
                    day_17 = table.Column<int>(nullable: false),
                    day_18 = table.Column<int>(nullable: false),
                    day_19 = table.Column<int>(nullable: false),
                    day_20 = table.Column<int>(nullable: false),
                    day_21 = table.Column<int>(nullable: false),
                    day_22 = table.Column<int>(nullable: false),
                    day_23 = table.Column<int>(nullable: false),
                    day_24 = table.Column<int>(nullable: false),
                    day_25 = table.Column<int>(nullable: false),
                    day_26 = table.Column<int>(nullable: false),
                    day_27 = table.Column<int>(nullable: false),
                    day_28 = table.Column<int>(nullable: false),
                    day_29 = table.Column<int>(nullable: false),
                    day_30 = table.Column<int>(nullable: false),
                    day_31 = table.Column<int>(nullable: false),
                    day_32 = table.Column<int>(nullable: false),
                    day_33 = table.Column<int>(nullable: false),
                    day_34 = table.Column<int>(nullable: false),
                    day_35 = table.Column<int>(nullable: false),
                    day_36 = table.Column<int>(nullable: false),
                    day_37 = table.Column<int>(nullable: false),
                    day_38 = table.Column<int>(nullable: false),
                    day_39 = table.Column<int>(nullable: false),
                    day_40 = table.Column<int>(nullable: false),
                    day_41 = table.Column<int>(nullable: false),
                    day_42 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateNumbers_Months_Month_ModelId",
                        column: x => x.Month_ModelId,
                        principalTable: "Months",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DateOccupancy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Month_ModelId = table.Column<int>(nullable: false),
                    day_1 = table.Column<int>(nullable: false),
                    day_2 = table.Column<int>(nullable: false),
                    day_3 = table.Column<int>(nullable: false),
                    day_4 = table.Column<int>(nullable: false),
                    day_5 = table.Column<int>(nullable: false),
                    day_6 = table.Column<int>(nullable: false),
                    day_7 = table.Column<int>(nullable: false),
                    day_8 = table.Column<int>(nullable: false),
                    day_9 = table.Column<int>(nullable: false),
                    day_10 = table.Column<int>(nullable: false),
                    day_11 = table.Column<int>(nullable: false),
                    day_12 = table.Column<int>(nullable: false),
                    day_13 = table.Column<int>(nullable: false),
                    day_14 = table.Column<int>(nullable: false),
                    day_15 = table.Column<int>(nullable: false),
                    day_16 = table.Column<int>(nullable: false),
                    day_17 = table.Column<int>(nullable: false),
                    day_18 = table.Column<int>(nullable: false),
                    day_19 = table.Column<int>(nullable: false),
                    day_20 = table.Column<int>(nullable: false),
                    day_21 = table.Column<int>(nullable: false),
                    day_22 = table.Column<int>(nullable: false),
                    day_23 = table.Column<int>(nullable: false),
                    day_24 = table.Column<int>(nullable: false),
                    day_25 = table.Column<int>(nullable: false),
                    day_26 = table.Column<int>(nullable: false),
                    day_27 = table.Column<int>(nullable: false),
                    day_28 = table.Column<int>(nullable: false),
                    day_29 = table.Column<int>(nullable: false),
                    day_30 = table.Column<int>(nullable: false),
                    day_31 = table.Column<int>(nullable: false),
                    day_32 = table.Column<int>(nullable: false),
                    day_33 = table.Column<int>(nullable: false),
                    day_34 = table.Column<int>(nullable: false),
                    day_35 = table.Column<int>(nullable: false),
                    day_36 = table.Column<int>(nullable: false),
                    day_37 = table.Column<int>(nullable: false),
                    day_38 = table.Column<int>(nullable: false),
                    day_39 = table.Column<int>(nullable: false),
                    day_40 = table.Column<int>(nullable: false),
                    day_41 = table.Column<int>(nullable: false),
                    day_42 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateOccupancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateOccupancy_Months_Month_ModelId",
                        column: x => x.Month_ModelId,
                        principalTable: "Months",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    PicoUnit = table.Column<int>(nullable: false),
                    picoUnitId = table.Column<int>(nullable: false),
                    picoUnitPhotoUrl = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RequestedDays = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NoOfNights = table.Column<int>(nullable: false),
                    Rent = table.Column<float>(nullable: false),
                    DownPayment = table.Column<float>(nullable: false),
                    Paid_InFull = table.Column<int>(nullable: false),
                    BookingAlertSent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_PicoUnits_picoUnitId",
                        column: x => x.picoUnitId,
                        principalTable: "PicoUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    DateRead = table.Column<DateTime>(nullable: true),
                    MessageSent = table.Column<DateTime>(nullable: false),
                    SenderDeleted = table.Column<bool>(nullable: false),
                    RecipientDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_picoUnitId",
                table: "Appointments",
                column: "picoUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_userId",
                table: "Appointments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_DateNumbers_Month_ModelId",
                table: "DateNumbers",
                column: "Month_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DateOccupancy_Month_ModelId",
                table: "DateOccupancy",
                column: "Month_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DateNumbers");

            migrationBuilder.DropTable(
                name: "DateOccupancy");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PicoUnits");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
