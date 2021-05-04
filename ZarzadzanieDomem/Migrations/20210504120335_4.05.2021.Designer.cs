﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210504120335_4.05.2021")]
    partial class _4052021
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ZarzadzanieDomem.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("HomeId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfExpense")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfExpenseId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.HasIndex("HomeId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.Home", b =>
                {
                    b.Property<int>("HomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HomeName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PostCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Street")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("HomeId");

                    b.ToTable("Homes");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.TypeOfExpense", b =>
                {
                    b.Property<int>("TypeOfExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TypeOfExpenseId");

                    b.ToTable("TypesOfExpenses");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<decimal>("ExpenseLimit")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("HomeId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("HomeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.Expense", b =>
                {
                    b.HasOne("ZarzadzanieDomem.Models.Home", null)
                        .WithMany("Expenses")
                        .HasForeignKey("HomeId");

                    b.HasOne("ZarzadzanieDomem.Models.User", null)
                        .WithMany("Expenses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.User", b =>
                {
                    b.HasOne("ZarzadzanieDomem.Models.Home", null)
                        .WithMany("Users")
                        .HasForeignKey("HomeId");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.Home", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ZarzadzanieDomem.Models.User", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}