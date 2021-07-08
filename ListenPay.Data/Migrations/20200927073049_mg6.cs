using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListenPay.Data.Migrations
{
    public partial class mg6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YouTubeVideo_UserInformation_UserInformationId",
                table: "YouTubeVideo");

            migrationBuilder.DropTable(
                name: "UserRelatedYouTubeCategories");

            migrationBuilder.DropIndex(
                name: "IX_YouTubeVideo_UserInformationId",
                table: "YouTubeVideo");

            migrationBuilder.DropColumn(
                name: "UserInformationId",
                table: "YouTubeVideo");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    LastPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRelatedYouTubeCategories",
                columns: table => new
                {
                    UserRelatedYouTubeCategoriesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(nullable: true),
                    UserCreated = table.Column<int>(nullable: true),
                    DateEntryModified = table.Column<DateTime>(nullable: true),
                    UserModified = table.Column<int>(nullable: true),
                    YouTubeCategoryId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRelatedYouTubeCategories", x => x.UserRelatedYouTubeCategoriesId);
                    table.ForeignKey(
                        name: "FK_CompanyRelatedYouTubeCategories_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRelatedYouTubeCategories_YouTubeVideoCategory_YouTubeCategoryId",
                        column: x => x.YouTubeCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRelatedYouTubeCategories_CompanyId",
                table: "CompanyRelatedYouTubeCategories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRelatedYouTubeCategories_YouTubeCategoryId",
                table: "CompanyRelatedYouTubeCategories",
                column: "YouTubeCategoryId");
            migrationBuilder.InsertData(table: "Company",
    columns: new[] { "FirstName", "LastName", "Email", "Password" },
    values: new object[] { "record","1234","recordTest@gmail.com","record123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyRelatedYouTubeCategories");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.AddColumn<int>(
                name: "UserInformationId",
                table: "YouTubeVideo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserRelatedYouTubeCategories",
                columns: table => new
                {
                    UserRelatedYouTubeCategoriesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateEntryCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEntryModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<int>(type: "int", nullable: true),
                    UserInformationId = table.Column<int>(type: "int", nullable: false),
                    UserModified = table.Column<int>(type: "int", nullable: true),
                    YouTubeCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelatedYouTubeCategories", x => x.UserRelatedYouTubeCategoriesId);
                    table.ForeignKey(
                        name: "FK_UserRelatedYouTubeCategories_UserInformation_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformation",
                        principalColumn: "UserInformationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRelatedYouTubeCategories_YouTubeVideoCategory_YouTubeCategoryId",
                        column: x => x.YouTubeCategoryId,
                        principalTable: "YouTubeVideoCategory",
                        principalColumn: "YouTubeVideoCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeVideo_UserInformationId",
                table: "YouTubeVideo",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedYouTubeCategories_UserInformationId",
                table: "UserRelatedYouTubeCategories",
                column: "UserInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRelatedYouTubeCategories_YouTubeCategoryId",
                table: "UserRelatedYouTubeCategories",
                column: "YouTubeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_YouTubeVideo_UserInformation_UserInformationId",
                table: "YouTubeVideo",
                column: "UserInformationId",
                principalTable: "UserInformation",
                principalColumn: "UserInformationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
