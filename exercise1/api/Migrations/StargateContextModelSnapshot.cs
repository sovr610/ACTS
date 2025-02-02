﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StargateAPI.Business.Data;

#nullable disable

namespace StargateAPI.Migrations
{
    [DbContext(typeof(StargateContext))]
    partial class StargateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StargateAPI.Business.Data.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AccountCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("StargateAPI.Business.Data.AstronautDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CareerEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CareerStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentDutyTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentRank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("AstronautDetail");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CareerStartDate = new DateTime(2019, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1896),
                            CurrentDutyTitle = "Commander",
                            CurrentRank = "1LT",
                            PersonId = 1
                        },
                        new
                        {
                            Id = 2,
                            CareerStartDate = new DateTime(2021, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1947),
                            CurrentDutyTitle = "Mission Specialist",
                            CurrentRank = "CAPT",
                            PersonId = 2
                        },
                        new
                        {
                            Id = 3,
                            CareerStartDate = new DateTime(2017, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1950),
                            CurrentDutyTitle = "Flight Engineer",
                            CurrentRank = "MAJ",
                            PersonId = 3
                        },
                        new
                        {
                            Id = 4,
                            CareerStartDate = new DateTime(2014, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1952),
                            CurrentDutyTitle = "Payload Specialist",
                            CurrentRank = "COL",
                            PersonId = 4
                        },
                        new
                        {
                            Id = 5,
                            CareerStartDate = new DateTime(2022, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1954),
                            CurrentDutyTitle = "Pilot",
                            CurrentRank = "2LT",
                            PersonId = 5
                        },
                        new
                        {
                            Id = 6,
                            CareerStartDate = new DateTime(2020, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1958),
                            CurrentDutyTitle = "Mission Specialist",
                            CurrentRank = "CAPT",
                            PersonId = 6
                        },
                        new
                        {
                            Id = 7,
                            CareerStartDate = new DateTime(2018, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1960),
                            CurrentDutyTitle = "Flight Surgeon",
                            CurrentRank = "MAJ",
                            PersonId = 7
                        },
                        new
                        {
                            Id = 8,
                            CareerStartDate = new DateTime(2015, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1963),
                            CurrentDutyTitle = "Commander",
                            CurrentRank = "LTC",
                            PersonId = 8
                        },
                        new
                        {
                            Id = 9,
                            CareerStartDate = new DateTime(2021, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1965),
                            CurrentDutyTitle = "Mission Specialist",
                            CurrentRank = "1LT",
                            PersonId = 9
                        },
                        new
                        {
                            Id = 10,
                            CareerStartDate = new DateTime(2019, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1968),
                            CurrentDutyTitle = "Pilot",
                            CurrentRank = "CAPT",
                            PersonId = 10
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Data.AstronautDuty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DutyEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DutyStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DutyTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("AstronautDuty");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DutyEndDate = new DateTime(2025, 4, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2001),
                            DutyStartDate = new DateTime(2024, 4, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1997),
                            DutyTitle = "Commander",
                            PersonId = 1,
                            Rank = "1LT"
                        },
                        new
                        {
                            Id = 2,
                            DutyEndDate = new DateTime(2025, 7, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2008),
                            DutyStartDate = new DateTime(2024, 7, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2006),
                            DutyTitle = "Mission Specialist",
                            PersonId = 2,
                            Rank = "CAPT"
                        },
                        new
                        {
                            Id = 3,
                            DutyEndDate = new DateTime(2025, 1, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2013),
                            DutyStartDate = new DateTime(2023, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2010),
                            DutyTitle = "Flight Engineer",
                            PersonId = 3,
                            Rank = "MAJ"
                        },
                        new
                        {
                            Id = 4,
                            DutyEndDate = new DateTime(2025, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2017),
                            DutyStartDate = new DateTime(2024, 1, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2015),
                            DutyTitle = "Payload Specialist",
                            PersonId = 4,
                            Rank = "COL"
                        },
                        new
                        {
                            Id = 5,
                            DutyEndDate = new DateTime(2025, 9, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2021),
                            DutyStartDate = new DateTime(2024, 9, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2019),
                            DutyTitle = "Pilot",
                            PersonId = 5,
                            Rank = "2LT"
                        },
                        new
                        {
                            Id = 6,
                            DutyEndDate = new DateTime(2025, 6, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2027),
                            DutyStartDate = new DateTime(2024, 6, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2025),
                            DutyTitle = "Mission Specialist",
                            PersonId = 6,
                            Rank = "CAPT"
                        },
                        new
                        {
                            Id = 7,
                            DutyEndDate = new DateTime(2025, 3, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2031),
                            DutyStartDate = new DateTime(2024, 3, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2029),
                            DutyTitle = "Flight Surgeon",
                            PersonId = 7,
                            Rank = "MAJ"
                        },
                        new
                        {
                            Id = 8,
                            DutyEndDate = new DateTime(2024, 12, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2035),
                            DutyStartDate = new DateTime(2023, 12, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2033),
                            DutyTitle = "Commander",
                            PersonId = 8,
                            Rank = "LTC"
                        },
                        new
                        {
                            Id = 9,
                            DutyEndDate = new DateTime(2025, 8, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2039),
                            DutyStartDate = new DateTime(2024, 8, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2038),
                            DutyTitle = "Mission Specialist",
                            PersonId = 9,
                            Rank = "1LT"
                        },
                        new
                        {
                            Id = 10,
                            DutyEndDate = new DateTime(2025, 5, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2045),
                            DutyStartDate = new DateTime(2024, 5, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2043),
                            DutyTitle = "Pilot",
                            PersonId = 10,
                            Rank = "CAPT"
                        },
                        new
                        {
                            Id = 11,
                            DutyEndDate = new DateTime(2025, 2, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2049),
                            DutyStartDate = new DateTime(2024, 2, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2047),
                            DutyTitle = "Flight Engineer",
                            PersonId = 11,
                            Rank = "MAJ"
                        },
                        new
                        {
                            Id = 12,
                            DutyEndDate = new DateTime(2024, 11, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2053),
                            DutyStartDate = new DateTime(2023, 11, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2051),
                            DutyTitle = "Payload Specialist",
                            PersonId = 12,
                            Rank = "LTC"
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Data.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jane Doe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Alice Johnson"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bob Smith"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Carol Williams"
                        },
                        new
                        {
                            Id = 6,
                            Name = "David Brown"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Eva Davis"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Frank Miller"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Grace Wilson"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Henry Taylor"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Ivy Anderson"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Jack Thompson"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Kelly Martinez"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Liam Harris"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Mia Clark"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Noah Lewis"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Olivia Lee"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Peter White"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Quinn Moore"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Rachel Green"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Samuel King"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Tina Turner"
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Data.AstronautDetail", b =>
                {
                    b.HasOne("StargateAPI.Business.Data.Person", "Person")
                        .WithOne("AstronautDetail")
                        .HasForeignKey("StargateAPI.Business.Data.AstronautDetail", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("StargateAPI.Business.Data.AstronautDuty", b =>
                {
                    b.HasOne("StargateAPI.Business.Data.Person", "Person")
                        .WithMany("AstronautDuties")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("StargateAPI.Business.Data.Person", b =>
                {
                    b.Navigation("AstronautDetail");

                    b.Navigation("AstronautDuties");
                });
#pragma warning restore 612, 618
        }
    }
}
