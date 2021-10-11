﻿// <auto-generated />
using System;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dating.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210817180737_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DatingApp.API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("MessageSent")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("RecipientDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DatingApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsMain")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PublicId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DatingApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DatabaseRole")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("IBAN")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Interests")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Introduction")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("KnownAs")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LookingFor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("myPicoAPI.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BookingAlertSent")
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<float>("DownPayment")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("NoOfNights")
                        .HasColumnType("int");

                    b.Property<int>("Paid_InFull")
                        .HasColumnType("int");

                    b.Property<float>("Rent")
                        .HasColumnType("float");

                    b.Property<string>("RequestedDays")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<string>("comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("picoUnitPhotoUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("puUnitId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("puUnitId");

                    b.HasIndex("userId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("myPicoAPI.Models.Month_Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("PicoUnit")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("myPicoAPI.Models.dateNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Month_ModelId")
                        .HasColumnType("int");

                    b.Property<int>("day_1")
                        .HasColumnType("int");

                    b.Property<int>("day_10")
                        .HasColumnType("int");

                    b.Property<int>("day_11")
                        .HasColumnType("int");

                    b.Property<int>("day_12")
                        .HasColumnType("int");

                    b.Property<int>("day_13")
                        .HasColumnType("int");

                    b.Property<int>("day_14")
                        .HasColumnType("int");

                    b.Property<int>("day_15")
                        .HasColumnType("int");

                    b.Property<int>("day_16")
                        .HasColumnType("int");

                    b.Property<int>("day_17")
                        .HasColumnType("int");

                    b.Property<int>("day_18")
                        .HasColumnType("int");

                    b.Property<int>("day_19")
                        .HasColumnType("int");

                    b.Property<int>("day_2")
                        .HasColumnType("int");

                    b.Property<int>("day_20")
                        .HasColumnType("int");

                    b.Property<int>("day_21")
                        .HasColumnType("int");

                    b.Property<int>("day_22")
                        .HasColumnType("int");

                    b.Property<int>("day_23")
                        .HasColumnType("int");

                    b.Property<int>("day_24")
                        .HasColumnType("int");

                    b.Property<int>("day_25")
                        .HasColumnType("int");

                    b.Property<int>("day_26")
                        .HasColumnType("int");

                    b.Property<int>("day_27")
                        .HasColumnType("int");

                    b.Property<int>("day_28")
                        .HasColumnType("int");

                    b.Property<int>("day_29")
                        .HasColumnType("int");

                    b.Property<int>("day_3")
                        .HasColumnType("int");

                    b.Property<int>("day_30")
                        .HasColumnType("int");

                    b.Property<int>("day_31")
                        .HasColumnType("int");

                    b.Property<int>("day_32")
                        .HasColumnType("int");

                    b.Property<int>("day_33")
                        .HasColumnType("int");

                    b.Property<int>("day_34")
                        .HasColumnType("int");

                    b.Property<int>("day_35")
                        .HasColumnType("int");

                    b.Property<int>("day_36")
                        .HasColumnType("int");

                    b.Property<int>("day_37")
                        .HasColumnType("int");

                    b.Property<int>("day_38")
                        .HasColumnType("int");

                    b.Property<int>("day_39")
                        .HasColumnType("int");

                    b.Property<int>("day_4")
                        .HasColumnType("int");

                    b.Property<int>("day_40")
                        .HasColumnType("int");

                    b.Property<int>("day_41")
                        .HasColumnType("int");

                    b.Property<int>("day_42")
                        .HasColumnType("int");

                    b.Property<int>("day_5")
                        .HasColumnType("int");

                    b.Property<int>("day_6")
                        .HasColumnType("int");

                    b.Property<int>("day_7")
                        .HasColumnType("int");

                    b.Property<int>("day_8")
                        .HasColumnType("int");

                    b.Property<int>("day_9")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Month_ModelId");

                    b.ToTable("DateNumbers");
                });

            modelBuilder.Entity("myPicoAPI.Models.dateOccupancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Month_ModelId")
                        .HasColumnType("int");

                    b.Property<int>("day_1")
                        .HasColumnType("int");

                    b.Property<int>("day_10")
                        .HasColumnType("int");

                    b.Property<int>("day_11")
                        .HasColumnType("int");

                    b.Property<int>("day_12")
                        .HasColumnType("int");

                    b.Property<int>("day_13")
                        .HasColumnType("int");

                    b.Property<int>("day_14")
                        .HasColumnType("int");

                    b.Property<int>("day_15")
                        .HasColumnType("int");

                    b.Property<int>("day_16")
                        .HasColumnType("int");

                    b.Property<int>("day_17")
                        .HasColumnType("int");

                    b.Property<int>("day_18")
                        .HasColumnType("int");

                    b.Property<int>("day_19")
                        .HasColumnType("int");

                    b.Property<int>("day_2")
                        .HasColumnType("int");

                    b.Property<int>("day_20")
                        .HasColumnType("int");

                    b.Property<int>("day_21")
                        .HasColumnType("int");

                    b.Property<int>("day_22")
                        .HasColumnType("int");

                    b.Property<int>("day_23")
                        .HasColumnType("int");

                    b.Property<int>("day_24")
                        .HasColumnType("int");

                    b.Property<int>("day_25")
                        .HasColumnType("int");

                    b.Property<int>("day_26")
                        .HasColumnType("int");

                    b.Property<int>("day_27")
                        .HasColumnType("int");

                    b.Property<int>("day_28")
                        .HasColumnType("int");

                    b.Property<int>("day_29")
                        .HasColumnType("int");

                    b.Property<int>("day_3")
                        .HasColumnType("int");

                    b.Property<int>("day_30")
                        .HasColumnType("int");

                    b.Property<int>("day_31")
                        .HasColumnType("int");

                    b.Property<int>("day_32")
                        .HasColumnType("int");

                    b.Property<int>("day_33")
                        .HasColumnType("int");

                    b.Property<int>("day_34")
                        .HasColumnType("int");

                    b.Property<int>("day_35")
                        .HasColumnType("int");

                    b.Property<int>("day_36")
                        .HasColumnType("int");

                    b.Property<int>("day_37")
                        .HasColumnType("int");

                    b.Property<int>("day_38")
                        .HasColumnType("int");

                    b.Property<int>("day_39")
                        .HasColumnType("int");

                    b.Property<int>("day_4")
                        .HasColumnType("int");

                    b.Property<int>("day_40")
                        .HasColumnType("int");

                    b.Property<int>("day_41")
                        .HasColumnType("int");

                    b.Property<int>("day_42")
                        .HasColumnType("int");

                    b.Property<int>("day_5")
                        .HasColumnType("int");

                    b.Property<int>("day_6")
                        .HasColumnType("int");

                    b.Property<int>("day_7")
                        .HasColumnType("int");

                    b.Property<int>("day_8")
                        .HasColumnType("int");

                    b.Property<int>("day_9")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Month_ModelId");

                    b.ToTable("DateOccupancy");
                });

            modelBuilder.Entity("myPicoAPI.Models.picoUnit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountNo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BankAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BankName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Caretaker")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CaretakerMobile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("DiscountPercentage")
                        .HasColumnType("float");

                    b.Property<float>("HighSeasonRent")
                        .HasColumnType("float");

                    b.Property<string>("Iban")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("LowSeasonRent")
                        .HasColumnType("float");

                    b.Property<float>("MidSeasonRent")
                        .HasColumnType("float");

                    b.Property<string>("Swift")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ownerId")
                        .HasColumnType("int");

                    b.Property<string>("picoUnitNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UnitId");

                    b.ToTable("PicoUnits");
                });

            modelBuilder.Entity("DatingApp.API.Models.Message", b =>
                {
                    b.HasOne("DatingApp.API.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DatingApp.API.Models.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DatingApp.API.Models.Photo", b =>
                {
                    b.HasOne("DatingApp.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("myPicoAPI.Models.Appointment", b =>
                {
                    b.HasOne("myPicoAPI.Models.picoUnit", "pu")
                        .WithMany("Appointments")
                        .HasForeignKey("puUnitId");

                    b.HasOne("DatingApp.API.Models.User", null)
                        .WithMany("Appointments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("myPicoAPI.Models.dateNumber", b =>
                {
                    b.HasOne("myPicoAPI.Models.Month_Model", null)
                        .WithMany("DateNumbers")
                        .HasForeignKey("Month_ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("myPicoAPI.Models.dateOccupancy", b =>
                {
                    b.HasOne("myPicoAPI.Models.Month_Model", null)
                        .WithMany("DateOccupancy")
                        .HasForeignKey("Month_ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
