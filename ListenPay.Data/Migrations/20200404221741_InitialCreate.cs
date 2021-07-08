using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "UserInformation",
                columns: table => new
                {
                    UserInformationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Earned = table.Column<decimal>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    CurrentPassword = table.Column<string>(nullable: true),
                    NewPassword = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformation", x => x.UserInformationId);
                    table.ForeignKey(
                        name: "FK_UserInformation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_CountryId",
                table: "UserInformation",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
