using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StargateAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronautDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CurrentRank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDutyTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CareerStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CareerEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AstronautDetail_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AstronautDuty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DutyTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DutyStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DutyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautDuty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AstronautDuty_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Doe" },
                    { 3, "Alice Johnson" },
                    { 4, "Bob Smith" },
                    { 5, "Carol Williams" },
                    { 6, "David Brown" },
                    { 7, "Eva Davis" },
                    { 8, "Frank Miller" },
                    { 9, "Grace Wilson" },
                    { 10, "Henry Taylor" },
                    { 11, "Ivy Anderson" },
                    { 12, "Jack Thompson" },
                    { 13, "Kelly Martinez" },
                    { 14, "Liam Harris" },
                    { 15, "Mia Clark" },
                    { 16, "Noah Lewis" },
                    { 17, "Olivia Lee" },
                    { 18, "Peter White" },
                    { 19, "Quinn Moore" },
                    { 20, "Rachel Green" },
                    { 21, "Samuel King" },
                    { 22, "Tina Turner" }
                });

            migrationBuilder.InsertData(
                table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2019, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1896), "Commander", "1LT", 1 },
                    { 2, null, new DateTime(2021, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1947), "Mission Specialist", "CAPT", 2 },
                    { 3, null, new DateTime(2017, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1950), "Flight Engineer", "MAJ", 3 },
                    { 4, null, new DateTime(2014, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1952), "Payload Specialist", "COL", 4 },
                    { 5, null, new DateTime(2022, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1954), "Pilot", "2LT", 5 },
                    { 6, null, new DateTime(2020, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1958), "Mission Specialist", "CAPT", 6 },
                    { 7, null, new DateTime(2018, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1960), "Flight Surgeon", "MAJ", 7 },
                    { 8, null, new DateTime(2015, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1963), "Commander", "LTC", 8 },
                    { 9, null, new DateTime(2021, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1965), "Mission Specialist", "1LT", 9 },
                    { 10, null, new DateTime(2019, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1968), "Pilot", "CAPT", 10 }
                });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2001), new DateTime(2024, 4, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(1997), "Commander", 1, "1LT" },
                    { 2, new DateTime(2025, 7, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2008), new DateTime(2024, 7, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2006), "Mission Specialist", 2, "CAPT" },
                    { 3, new DateTime(2025, 1, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2013), new DateTime(2023, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2010), "Flight Engineer", 3, "MAJ" },
                    { 4, new DateTime(2025, 10, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2017), new DateTime(2024, 1, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2015), "Payload Specialist", 4, "COL" },
                    { 5, new DateTime(2025, 9, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2021), new DateTime(2024, 9, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2019), "Pilot", 5, "2LT" },
                    { 6, new DateTime(2025, 6, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2027), new DateTime(2024, 6, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2025), "Mission Specialist", 6, "CAPT" },
                    { 7, new DateTime(2025, 3, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2031), new DateTime(2024, 3, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2029), "Flight Surgeon", 7, "MAJ" },
                    { 8, new DateTime(2024, 12, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2035), new DateTime(2023, 12, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2033), "Commander", 8, "LTC" },
                    { 9, new DateTime(2025, 8, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2039), new DateTime(2024, 8, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2038), "Mission Specialist", 9, "1LT" },
                    { 10, new DateTime(2025, 5, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2045), new DateTime(2024, 5, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2043), "Pilot", 10, "CAPT" },
                    { 11, new DateTime(2025, 2, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2049), new DateTime(2024, 2, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2047), "Flight Engineer", 11, "MAJ" },
                    { 12, new DateTime(2024, 11, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2053), new DateTime(2023, 11, 17, 1, 39, 39, 752, DateTimeKind.Local).AddTicks(2051), "Payload Specialist", 12, "LTC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserName",
                table: "Account",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AstronautDetail_PersonId",
                table: "AstronautDetail",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AstronautDuty_PersonId",
                table: "AstronautDuty",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Id",
                table: "Person",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AstronautDetail");

            migrationBuilder.DropTable(
                name: "AstronautDuty");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
